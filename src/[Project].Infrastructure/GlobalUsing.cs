// External libraries
global using System.Reflection;
global using MediatR;
global using Microsoft.Extensions.DependencyInjection;

// Project namespaces
global using _Project_.Application.Interfaces.DomainEvents;
global using _Project_.Domain.Abstractions.Interfaces;
global using _Project_.Domain.DomainEvents;
global using _Project_.Infrastructure.DomainEvents;
global using _Project_.Application.Interfaces.IdempotencyStore;
global using _Project_.Infrastructure.IdempotencyStore;
global using _Project_.Application.Interfaces;
global using _Project_.Contracts.Abstractions.Message;
global using _Project_.Contracts.Abstractions.Shared;
global using _Project_.Infrastructure.MessageBus;
global using _Project_.Infrastructure.RequestContext;