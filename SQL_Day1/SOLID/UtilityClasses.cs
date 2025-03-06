using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID
{
    // Interface segregation and dependency inversion principles
    // ISP: The interface defines only what is necessary for payment processing.
    // DIP: High-level modules depend on this abstraction.
    public interface IPaymentProcessor
    {
        void ProcessPayment();
    }

    // Single Responsibility Principle (SRP)
    // This class handles only Credit Card payments.
    public class CreditCardPayment : IPaymentProcessor
    {
        public void ProcessPayment()
        {
            Console.WriteLine("Processing Credit Card payment...");
        }
    }

    // SRP: This class handles only PayPal payments.
    public class PayPalPayment : IPaymentProcessor
    {
        public void ProcessPayment()
        {
            Console.WriteLine("Processing PayPal payment...");
        }
    }

    // SRP: This class handles only Bitcoin payments.
    public class BitcoinPayment : IPaymentProcessor
    {
        public void ProcessPayment()
        {
            Console.WriteLine("Processing Bitcoin payment...");
        }
    }


    // Open/Closed Principle (OCP)
    // The factory is open for extension by adding new types, but closed for modification of existing logic.
    public class PaymentProcessorFactory
    {
        public static IPaymentProcessor GetPaymentProcessor(string paymentType)
        {
            return paymentType switch
            {
                "CreditCard" => new CreditCardPayment(),
                "PayPal" => new PayPalPayment(),
                "Bitcoin" => new BitcoinPayment(),
                _ => throw new NotSupportedException($"Payment type '{paymentType}' is not supported.")
            };
        }
    }

    // SRP: This class orchestrates the payment process but delegates the actual work to the respective processors.
    // DIP: It depends on the IPaymentProcessor abstraction, not concrete implementations.
    public class PaymentProcessor
    {
        private readonly IPaymentProcessor _paymentProcessor;

        public PaymentProcessor(IPaymentProcessor paymentProcessor)
        {
            _paymentProcessor = paymentProcessor;
        }

        public void ProcessPayment()
        {
            _paymentProcessor.ProcessPayment();
        }
    }


}
