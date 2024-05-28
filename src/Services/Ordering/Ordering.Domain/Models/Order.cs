﻿
namespace Ordering.Domain.Models;

public class Order : Aggregate<OrderItem>
{
    private readonly List<OrderItem> _orderItems = new();

    public IReadOnlyList<OrderItem> OrderItems  => _orderItems.AsReadOnly();

    public CustomerId CustomerId { get; private set; } = default!;

    public OrderName OrdersName { get; private set; } = default!;

    public Address ShippingAddress { get; private set; } = default!;
    public Address BillingAddress { get; private set; } = default!;
    public Payment Payment { get; private set; } = default!;
    public OrdeStatus Status { get; private set; } = OrdeStatus.Pending;

    public decimal TotalPrice
    {
        get => OrderItems.Sum(x => x.Price * x.Quantity);
        private set { }
    }


}

