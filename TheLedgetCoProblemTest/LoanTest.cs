using System;
using TheLedgerCoProblem.Model;
using Xunit;

namespace TheLedgetCoProblemTest
{
    public class LoanTest
    {
        

        [Fact]
        public void ItShouldBeCreateNewInstaceOfLoanWithCalucltionOfLoanDetails()
        {
            string bankName = "XYZ";
            int principle = 5000;
            int tenure = 1;
            int interestRate = 6;
            Loan loan = new Loan(bankName, principle, tenure, interestRate);
            var totalInterest = Math.Ceiling(Convert.ToDouble((principle * tenure * interestRate) / 100));
            var expectedActualPay = Convert.ToDouble(principle + totalInterest);
            var expectedEmiAmount = Math.Ceiling(expectedActualPay / (tenure * 12));
            Assert.Equal(expectedActualPay, loan.ActualPay);
            Assert.Equal(expectedEmiAmount, loan.EmiAmount);
            Assert.Equal(loan.TotalEmi, tenure * 12);

        }
        [Fact]
        public void ItShouldBeCalculateBalanceAccordingToEmiNo()
        {
            Loan loan = new Loan("XYZ", 5000, 1, 6);
            loan.CalculateBalance(3);
            var expectedTotalPaid = loan.EmiAmount * 3;
            Assert.Equal(loan.TotalPaid, expectedTotalPaid);
            Assert.Equal(loan.RemainingOutstanding, loan.ActualPay - expectedTotalPaid);

        }
        [Fact]
        public void ItShouldBeCalculateBalanceAccordingToEmiNoIfAnyPaymentHappenAfter()
        {
            Loan loan = new Loan("XYZ", 5000, 1, 6);
            Payment pay = new Payment(1000, 5);
            loan.Payments.Add(pay);
            loan.CalculateBalance(3);
            var expectedTotalPaid = loan.EmiAmount * 3;
            Assert.Equal(loan.TotalPaid, expectedTotalPaid);
            Assert.Equal(loan.RemainingOutstanding, loan.ActualPay - expectedTotalPaid);

        }
        [Fact]
        public void ItShouldBeCalculateBalanceAccordingToEmiNoIfAnyPaymentHappenBefore()
        {
            Loan loan = new Loan("XYZ", 5000, 1, 6);
            Payment pay = new Payment(1000, 5);
            loan.Payments.Add(pay);
            loan.CalculateBalance(6);
            var expectedTotalPaid = (loan.EmiAmount * 6) + pay.LumpSumAmount;
            Assert.Equal(expectedTotalPaid, loan.TotalPaid);
            Assert.Equal(loan.RemainingOutstanding, loan.ActualPay - expectedTotalPaid);
        }

        [Fact]
        public void IfRemainingOutstandingIsLessThanEmiAmoutThenEmiOutShouldBeEqualsToRemainingAmount()
        {
            Loan loan = new Loan("XYZ", 5000, 1, 6);
            loan.RemainingOutstanding = 14;
            loan.CalculateBalance(1);
            loan.RemainingOutstanding = 14;
            Assert.Equal(loan.EmiAmount, loan.RemainingOutstanding);
        }


    }
}
