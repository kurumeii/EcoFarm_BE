﻿using Ardalis.Result;
using EcoFarm.Application.Interfaces.Messagings;
using FluentValidation;
using MediatR;

namespace EcoFarm.Api.Abstraction.Behaviors
{
    public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!_validators.Any())
            {
                return await next();
            }
            Ardalis.Result.Result result = new();

            var context = new ValidationContext<TRequest>(request);
            var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
            var errorsDictionary = validationResults
                .SelectMany(validationResult => validationResult.Errors)
                .Where(failure => failure is not null)
                .Distinct();
            //.GroupBy(
            //    x => x.PropertyName,
            //    x => x.ErrorMessage,
            //    (propertyName, errorMessages) => new
            //    {
            //        Key = propertyName,
            //        Values = errorMessages.Distinct().ToArray()
            //    })
            //.ToDictionary(x => x.Key, x => x.Values);
            if (errorsDictionary.Any())
            {
                throw new ValidationException(errorsDictionary);
            }
            return await next();
        }
    }
}
