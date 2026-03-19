using System.Linq.Expressions;

namespace Domain.Validation
{
    public class CostumerModelValidator<T>
    {
        public T Model { get; }

        private readonly List<string> _errorMessages = new List<string>();

        public CostumerModelValidator(T model)
        {
            Model = model;
        }

        public CostumerModelValidator<T> AddErrorMessage(string errorMessage)
        {
            _errorMessages.Add(errorMessage);
            return this;
        }

        public IEnumerable<string> GetErrorMessages()
        {
            return _errorMessages;
        }

        public PropertyRuleBuilder<T, TProperty> ValidateFor<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            var compiledExpression = expression.Compile();
            var propertyValue = compiledExpression(Model);
            return new PropertyRuleBuilder<T, TProperty>(this, propertyValue);
        }
    }

    public class PropertyRuleBuilder<T, TProperty>
    {
        private readonly CostumerModelValidator<T> _validator;
        private readonly TProperty _propertyValue;

        public PropertyRuleBuilder(CostumerModelValidator<T> validator, TProperty propertyValue)
        {
            _validator = validator;
            _propertyValue = propertyValue;
        }

        public PropertyRuleBuilder<T, TProperty> WithMessage(string message)
        {
            _validator.AddErrorMessage(message);
            return this;
        }

        public PropertyRuleBuilder<T, TProperty> MinLength(int minLength, string errorMessage)
        {
            var value = _propertyValue?.ToString();

            if (!string.IsNullOrEmpty(value) && value.Length < minLength)
                _validator.AddErrorMessage(errorMessage);

            return this;
        }

        public PropertyRuleBuilder<T, TProperty> MaxLength(int maxLength, string errorMessage)
        {
            var value = _propertyValue?.ToString();

            if (!string.IsNullOrEmpty(value) && value.Length > maxLength)
                _validator.AddErrorMessage(errorMessage);
            return this;
        }

        public PropertyRuleBuilder<T, TProperty> Contains(string value, string errorMessage)
        {
            string valueProperty = Convert.ToString(_propertyValue) ?? string.Empty;

            if (!string.IsNullOrEmpty(valueProperty) && !valueProperty.Contains(value))
                _validator.AddErrorMessage(errorMessage);

            return this;
        }

        public PropertyRuleBuilder<T, TProperty> SetPropertyIsRequired(string message)
        {
            if (string.IsNullOrEmpty(_propertyValue?.ToString()))
                _validator.AddErrorMessage(message);
            return this;
        }

        public PropertyRuleBuilder<T, TProperty> NotEmpty(string message)
        {
            if (string.IsNullOrWhiteSpace(_propertyValue?.ToString()))
                _validator.AddErrorMessage(message);
            return this;
        }

        public PropertyRuleBuilder<T, TProperty> NotNegative(string message)
        {
            if (_propertyValue is decimal decimalValue && decimalValue < 0)
                _validator.AddErrorMessage(message);
            return this;
        }
    }
}