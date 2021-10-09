# Lab 3 Domain Driven Design

* Deadline: 22.10.2021
* The lab has to be manually approved by the teaching assistants during lab hour.
  * The student is expected to explain the code during the manually approval.
  * All code has to be submitted before the deadline, but can be approved on the first lab after the deadline.
  * It is possible to get the code approved before the deadline.
* Lab 3 contains very few QuickFeed tests due to the nature of the code.


## Building a food ordering system (Campus eats)

For lab 3 you are going to build the ordering and fulfillment part of the food ordering system which takes and order from a customer and makes it available to a shipper which can bring the order to the customer. It would be a system similar to foodora or wolt. 

### General description

The customer is placing an order for an item from the campus cafeteria or the cafe and a shipper will go and buy the item, bring it to the desired location and received payment from the customer.

## Lab2

Under you can find the diagram we currently have from lab two. It is very basic and does not contain a lot of information. 

```mermaid 
classDiagram
    class FoodItem{
        <<Aggregate root>>
        + Name
        + Description
        + Price
        + Cooktime
    }
```

### New class diagram

From the lectures the current diagram was created with some modifications:

```mermaid
flowchart TD
    ProductContext -.FoodItemNameChanged.-> CartContext
    ProductContext -.FoodItemPriceChanged.-> CartContext
    CartContext --Checkout--> IOrderingService
    subgraph OrderContext
        IOrderingService --> Order
    end
    Order -.OrderPlaced.-> FulfillmentContext
    Order -.OrderPlaced.-> InvoicingContext
    FulfillmentContext -.OfferShipperSet.-> Order
```

### Product Context

```mermaid
classDiagram
    class FoodItem {
        <<Aggregate root>>
        + Name
        + Description
        + Price
        + Cooktime
    }
```

### Cart Context

```mermaid
classDiagram
    direction LR
    ShoppingCart --> CartItem
    class ShoppingCart {
        <<Aggregate root>>
        + Id
        + Items
        + AddItem()
    }
    class CartItem {
        + Id
        + Sku
        + Name
        + Price
        + Count
    }
```

### Ordering Context

```mermaid
classDiagram
    direction LR
    class IOrderingService {
        <<Service>>
        + PlaceOrder()
    }
    class Order {
        <<Aggregate root>>
        + Id
        + Date
        + OrderLines
        + Location
        + Notes
        + Customer
        + Status
        + AddOrderLine()
    }

    class OrderLine{
        + Id
        +Item
        +Price
        +Count
    }
    class Location {
        <<ValueObject>>
        + Building
        + RoomNumber
        + Notes
    }
    class Customer {
        + Id
        + Name
    }
    class Status {
        <<enum>>
        New
        Placed
        Shipped
        Delivered
        Missing
        Declined
    }
    IOrderingService --> Order
    Order --> Location
    Order --> OrderLine
    Order --> Customer
    Order --> Status
```

### Fulfillment

```mermaid
classDiagram
    direction LR
    class Offer {
        <<Aggregate root>>
        + OrderId
        + Shipper
    }
    class Shipper {
        + Name
    }
    class Reimbursement {
        + Shipper
        + Amount
        + InvoiceId
    }
    Offer --> Shipper
    Offer --> Reimbursement
```

### Invoicing

```mermaid
classDiagram
    direction LR
    class Invoice {
        <<Aggregate root>>
        + Customer
        + Address
        + Amount
        + OrderId
        + Status
    }
    class Payment {
        + Amount
    }
    class Customer {
        + Name
    }
    class Status {
        <<Enum>>
        New
        Paid
        Overdue
        Credited
    }
    class Address {
        <<ValueObject>>
        + Building
        + RoomNumber
        + Notes
    }

    Invoice --> Payment
    Invoice --> Customer
    Invoice --> Status
    Invoice --> Address
```

<!--![Products.svg](./docs/products.svg)-->

## Task

For this lab you are supposed to implement the parts of the diagram which is currently not implemented. Continuous lines are api class from a pipeline to a service, where the service should be in a different context from the pipeline, and dotted lines are event based messages sent across context boundaries. the products and cart implementation are given as an inspiration for how to implement the rest.

### Organizing the code

You should use the same folder structure as found in `Core/Domain/Cart` and `Core/Domain/Products`. Every class, interface, record and enum should be in its own file unless it is part of another class like for instance the pipeline handlers, where the request is defined in the same file as the handler. 

The code should follow the DDD principal learned in class as well as is already used in the Cart and Products folders. You should use Mediatr and the pipelines to handle context requests as shown in the Cart and Products folders. The the domain models is required to be stored to the database so they should be added to the ShopContext.

Folder structure:
```
Domain/
    {Context name}/
        Handlers/
            (Should contain event handlers)
        Pipelines/
            (Should contain Pipeline handlers and requests)
        Events/
            (Should contain the event class)
        Dto/
            (Should contain Data transfer objects)
        Services/
            (Should contain the interface and implementation of domain services)
        (The Domain models should be in the Context folder directly)
```

### Required parts for the lab

1. Create a new event handler for "FoodItemPriceChanged"
   1. Get inspiration from FoodItemNameChangedHandler and FoodItemNameChanged for how to implement it.
   2. This is so that if an item price is updated then also the cart price is updated.
2. Create the ordering context
   1. Create the order aggregated root and related classes as shown in the diagram
   2. Create a new class which implements IOrderingService
      1. This is a class which should have an PlaceOrder method which takes all the information needed to create an order, (Location, CustomerName, CartItems)
   3. Create a new Pipeline for CartCheckout (in the Cart context) which retrieves a cart and inserts it into the IOrderingService to create an order
      1. The current cart should be closed and removed from the session after it is placed.
   4. Update the UI to be able to place an order
      1. This ui has to contain the following information for placing the order
         1. Building
         2. Room number
         3. Location Notes
         4. Customer Name
      2. These are also the fields that the IOrderingService requires for creating an order
   5. Verify that it is possible to place and order
   6. Create an order overview for all placed orders
   7. Create a details page which can display details for an order, this page should be reached from the order overview page.
      1. This page should also request the Offer and Invoice information from the respective contexts and display them if they are implemented and available.
3. Create the Fulfillment context
   1. Create the Offer aggregated root and related classes as shown in the diagram
   2. When an OrderPlaced event is raised then a handler in the fulfillment context should create an empty offer
      1. The Reimbursement should be created without InvoiceId and Shipper with only the sum of the order specified.
      2. The Shipper should be null when the offer is created
   3. Update the order details page with a form which accepts a Shipper name as input
      1. This should only be displayed if the status of the order is Placed
      2. When submitted the page should send a message to the fulfillment pipeline which assigns the shipper to the offer.
      3. This should trigger an event which marks the order as sent.
         1. The event handler should be in the Ordering context
4. Create the Invoice context
   1. Create the Invoice Aggregated root and related classes as shown in the diagram
   2. When an OrderPlaced event is raised then a handler in the Invoice context should create an invoice and related classes
5. There should not be a way to construct an object without having to set required fields
   1. Look at ShoppingCart, CartItem and FoodItem for inspiration. 
   2. We will tolerate if not all items is created this way, but try as best as possible.


### New PriceChanged event handler

The first task is to create a new event handler for when a product price is updated. This should update all the prices in the shopping carts which is not completed yet, so that the customer see the updated price, instead of the price when the item was added to the cart.

### The ordering context

The OrderingContext should contain data and logic for handling everything about creating the order.

### The fulfillment context

The fulfillment context should contain data and logic for handling the order after the order context has created the order. This includes ability to set a shipper for the food item, as well as sending updates to the ordering context that someone has started working on the order.

### The invoice context

The invoice context should contain data and logic related to payment of the order. After the order is created this should handle things like sending the invoice to the customer as well as handling the payment information. In this lab, we will only create the invoice and display it as described in the task points at the top.

### Value objects and EF Core

This link describes how to define an object as an value object (Owned entity) in entity framework. [https://docs.microsoft.com/en-us/ef/core/modeling/owned-entities](https://docs.microsoft.com/en-us/ef/core/modeling/owned-entities).