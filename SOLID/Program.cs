using System;


namespace SOLID
{
    class Program
    {
        static void Main(string[] args)
        {
            // Example usage
            Console.WriteLine("Enter payment type (CreditCard, PayPal, Bitcoin):");
            string paymentType = Console.ReadLine();

            try
            {
                // OCP: Adding new payment types only requires extending the factory, not modifying existing code.
                IPaymentProcessor processor = PaymentProcessorFactory.GetPaymentProcessor(paymentType);

                // DIP: The PaymentProcessor works with the abstraction (IPaymentProcessor).
                PaymentProcessor paymentProcessor = new PaymentProcessor(processor);

                // LSP: Any implementation of IPaymentProcessor can be used here without changing the system's behavior.
                paymentProcessor.ProcessPayment();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

}