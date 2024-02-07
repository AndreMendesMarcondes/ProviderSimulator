namespace PS.Domain.DTO
{
    public class InvoiceResponse
    {
        public InvoiceResponse()
        {
            var random = new Random();
            Id = Guid.NewGuid();
            IssueDate = DateTime.Now;
            InvoiceItens = new List<InvoiceIten>();

            int numberOfItems = random.Next(1, 6);
            double totalValue = 0;

            for (int i = 0; i < numberOfItems; i++)
            {
                var item = new InvoiceIten
                {
                    Id = Guid.NewGuid(),
                    Description = $"Item {i + 1}",
                    Value = Math.Round(random.NextDouble() * 100, 2)
                };

                InvoiceItens.Add(item);
                totalValue += item.Value;
            }

            Value = totalValue; 
        }

        public Guid Id { get; set; }
        public DateTime IssueDate { get; set; }
        public double Value { get; set; }
        public List<InvoiceIten> InvoiceItens { get; set; }
    }

    public class InvoiceIten
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public double Value { get; set; }
    }
}
