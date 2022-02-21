namespace UsedCar.Backend.Domains.Users.ValueObjects
{
    public record IdaasId
    {
        public IdaasId(string idaasId)
        {
            if (string.IsNullOrEmpty(idaasId))
            {
                throw new ArgumentNullException(nameof(idaasId), "IdaasId can not be null.");
            }

            Value = idaasId;
        }
        public string Value { get; }
    }
}
