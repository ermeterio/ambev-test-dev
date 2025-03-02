﻿using Ambev.DeveloperEvaluation.Domain.Events.Interface;
using Rebus.Bus;

public class DomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IBus _bus;
    private readonly List<object> _events = new();

    public DomainEventDispatcher()
    {
    }

    public DomainEventDispatcher(IBus bus)
    {
        _bus = bus;
    }

    public void AddEvent(object domainEvent)
    {
        _events.Add(domainEvent);
    }

    public async Task DispatchEventsAsync()
    {
        foreach (var domainEvent in _events)
        {
            await _bus.Publish(domainEvent);
        }
        _events.Clear();
    }
}