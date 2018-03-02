using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Storage.Converters;
namespace Models
{
    public class CreditStateValueConverter : ValueConverter<State, string>
    {
        public CreditStateValueConverter(
            Expression<Func<State, string>> convertToStoreExpression, 
            Expression<Func<string, State>> convertFromStoreExpression, 
            ConverterMappingHints mappingHints = default(ConverterMappingHints)) : 
            base(convertToStoreExpression, convertFromStoreExpression, mappingHints)
        {
            
        }

        public static CreditStateValueConverter Create(
            Expression<Func<State, string>> convertToStoreExpression,
            Expression<Func<string, State>> convertFromStoreExpression,
            ConverterMappingHints mappingHints = default(ConverterMappingHints))
        {
            return new CreditStateValueConverter(convertToStoreExpression, convertFromStoreExpression, mappingHints);
        }
    }
}
