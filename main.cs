using System;
using System.Collections.Generic;

class Program {
  public static void Main (string[] args) {
   List <Loan> list = new List <Loan>();
   int choice;
   while (true){
     Console.WriteLine("Menu:");
       Console.WriteLine("1 - Add Loan");
       Console.WriteLine("2 - Delete Loan");
       Console.WriteLine("3 - Calculate Monthly Payment");
       Console.WriteLine("4 - Print Loans");
       Console.WriteLine("5 - Exit");
       choice = Convert.ToInt32(Console.ReadLine());
       switch (choice){
         case 1:
         addLoan(list);
         break;
         case 2:
         deleteLoan(list);
         break;
         case 3: 
         calculateMonthlyLoanPayment(list);
         break;
         case 4:
         printLoans(list);
         break;
         case 5:
         System.Environment.Exit(0);
         break;
       }
     }
   }

   //methods
   static void addLoan(List<Loan> list){
     Console.WriteLine("Enter Loan Type: (1 – Student, 2 – Auto)");
     int Type = Convert.ToInt32(Console.ReadLine());
     Console.WriteLine("Enter Customer Name: ");
     string name = Console.ReadLine();
     Console.WriteLine("Enter Loan Amount: ");
     double amount = Convert.ToDouble(Console.ReadLine());
     Console.WriteLine("Enter Interst Rate: ");
     double rate = Convert.ToDouble(Console.ReadLine());
     Console.WriteLine("Enter Number of Monthly Payments: ");
     int month = Convert.ToInt32(Console.ReadLine());
     if(Type == 1){
       bool def;
       Console.WriteLine("Is Loan Deferred? (Y or N): ");
       string choice = Console.ReadLine();
       if(choice == "Y" || choice == "y"){
          def = true;
       }
       else {
          def = false;
       }
       list.Add(new StudentLoan(name, amount, rate, month, def));
     }
     else{
       Console.WriteLine("Enter Amount of Down Payment: ");
         double down = Convert.ToDouble(Console.ReadLine());
         list.Add(new AutoLoan(name, amount, rate, month, down));
     }
    
   }
   static void deleteLoan(List<Loan> list){
     Console.WriteLine("Enter a Customer Name: ");
     string name = Console.ReadLine();
     for (int i = 0; i < list.Count; i++){
       Loan index = list[i];
       if (index.getCustomerName() == name){
         list.RemoveAt(i);
       }
     }
   }
   static void calculateMonthlyLoanPayment(List<Loan> list){
     for (int i = 0; i < list.Count; i++){
       Loan index = list[i];
       index.calculateMonthlyPayment();
     }
   }
   static void printLoans(List<Loan> list){
     for (int i = 0; i < list.Count; i++){
       Loan index = list[i];
       Console.WriteLine(index);
       Console.WriteLine();
      }
   }
  class Loan{
    private string customerName;
    private int accountNumber; 
    private double loanBalance;
    private double monthlyPayment;
    private double interestRate;
    private int numberOfMonthlyPayments;
    private static int numberOfLoans = 0;
    public Loan(string name, double amount, double rate, int months){
      this.customerName = name;
      this.loanBalance = amount;
      this.interestRate = rate;
      this.numberOfMonthlyPayments = months;
      this.accountNumber = numberOfLoans++;
    }
    public static int getNumberOfLoans(){
      return numberOfLoans;
    }
    public void setCustomerName(string s){
      customerName = s;
    }
    public string getCustomerName(){
      return customerName;
    }
    public void setLoanBalance(double x){
      loanBalance = x;
    }
    public double getLoanBalance(){
      return loanBalance;
    }
    public void setInterestRate(double x){
      interestRate = x;
    }
    public double getInterestRate(){
      return interestRate;
    }
    public void setNumberOfMonthlyPayments(int x){
      numberOfMonthlyPayments = x;
    }
    public int getNumberOfMonthlyPayments(){
      return numberOfMonthlyPayments;
    }
    public static void decreaseNumberOfLoans(){
      numberOfLoans --;
    }
    public virtual void  calculateMonthlyPayment(){
      monthlyPayment = ((interestRate*loanBalance) / 
      Math.Pow(1 - (1 + interestRate), -numberOfMonthlyPayments));
    }
    public void setMonthlyPayment(double a){
      monthlyPayment = a;
    }
    public override string ToString(){
      return "Customer Name: " + customerName + ", Account number: " + accountNumber + ", Loan Balance: " + loanBalance + ", Monthly Payment: " + monthlyPayment + ", Interst Rate: " + interestRate + ", Number of Monthly Payments: " + numberOfMonthlyPayments; 
    }
  }
  class StudentLoan : Loan {
    private bool isDeferred;
    public StudentLoan(string name, double amount, double rate, int months, bool isDef) : base(name, amount, rate, months){
      this.isDeferred = isDef;
    }
    public void setIsDeferred(bool b){
      isDeferred = b;
    }
    public bool getIsDeferred(){
      return isDeferred;
    }
    public override void calculateMonthlyPayment(){
      if(isDeferred){
        base.setMonthlyPayment(0);
      }
      else {
        base.calculateMonthlyPayment();
      }
    }
    public override string ToString(){
      return base.ToString() + ", Is Deffered: " + isDeferred;
    }
  }
  class AutoLoan : Loan {
    private double downPayment;
    public AutoLoan (string name, double amount, double rate, int months, double down) : base(name, amount, rate, months){
      downPayment = down;
    }
    public void setDownPayment(double d){
      downPayment = d;
    }
    public double getDownPayment(){
      return downPayment;
    }
    public override void calculateMonthlyPayment(){
      setMonthlyPayment(((getInterestRate()*(getLoanBalance() - downPayment)) / 
      Math.Pow(1 - (1 + getInterestRate()), -getNumberOfMonthlyPayments())));
    }
    public override string ToString(){
      return base.ToString() + ", Down Payment: " + downPayment;
    }
  }
}