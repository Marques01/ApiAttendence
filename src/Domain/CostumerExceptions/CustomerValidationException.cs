namespace Domain.CostumerExceptions
{
    public class CustomerValidationException(IEnumerable<string> errorMessages) : Exception
    {
        public IEnumerable<string> ErrorMessages { get; } = errorMessages;
    }
}