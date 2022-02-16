namespace UsedCar.Backend.Domains.Users.ValueObjects
{
    public record IDassId
    {
        public IDassId(string idassid)
        {
            if (string.IsNullOrEmpty(idassid))
            {
                throw new ArgumentNullException(nameof(idassid), "idassid can not be null.");
            }

            Value = idassid;
        }
        public string Value { get; }
    }
}
