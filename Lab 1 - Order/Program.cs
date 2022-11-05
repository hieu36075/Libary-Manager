using System;
using System.Collections.Generic;
using System.Linq;

namespace Asm2_1651
{

	abstract class Person
	{
		string name;
		string address;
		string numberPhone;

		public string Name { get => name; set => name = value; }
		public string Address { get => address; set => address = value; }
		public string NumberPhone { get => numberPhone; set => numberPhone = value; }

		public Person(string name, string address, string numberPhone)
		{
			this.Name = name;
			this.Address = address;
			this.NumberPhone = numberPhone;
		}
	}


	class Customer : Person
	{
		private static int m_Counter = 1;
		int idCustomer;
		List<Order> orders = new List<Order>();
		Admin admin;

		public int IdCustomer { get => idCustomer; }
		internal Admin Admin { get => admin; set => admin = value; }
		internal List<Order> Orders { get => orders; set => orders = value; }

		public Customer(Admin admin, string name, string address, string numberPhone) :
			base(name, address, numberPhone)
		{
			this.Admin = admin;
			this.idCustomer = m_Counter++;
		}

		public string ShowAll()
		{
			string a = "";
			foreach (var item in Orders)
			{
				a += $"{item.ToString()}";
			}
			return  a ;
		}
		public override string ToString()
		{
			return $"ID Customer: {IdCustomer} -- Name: {Name} -- Address: {Address} -- Phone: {NumberPhone}" +
				$" \n {ShowAll()}";
		}

		public void AddOrder(Order order)
		{
			Orders.Add(order);
			Console.WriteLine("Add Order successful");
			Console.WriteLine(" ");
		}

		public Order SearchOrder(int id)
		{
			var Order = Orders.FirstOrDefault(a => a.IdOrder.Equals(id));
			return Order;
		}

		public bool RemoveOrder(Order order)
		{
			var Order = Orders.FirstOrDefault(a => a.IdOrder.Equals(order.IdOrder));
			if (Order == null)
			{
				Console.WriteLine("No Information");
				return false;
			}
			else
			{
				Orders.Remove(Order);
				Console.WriteLine("Remove successful");
				return true;
			}
		}
    }

	class Order
	{
		private static int m_Counter = 1;
		int idOrder;
		Customer customer;
		string startDate;
		string endDate;
		private List<OrderDetail> orderDetails = new List<OrderDetail>();

		public int IdOrder { get => idOrder; }

		public string StartDate { get => startDate; set => startDate = value; }

		public string EndDate { get => endDate; set => endDate = value; }

		internal Customer Customer { get => customer; set => customer = value; }
		internal List<OrderDetail> OrderDetails { get => orderDetails; set => orderDetails = value; }

		public Order( Customer customer)
		{
			this.idOrder = m_Counter++;
			this.Customer = customer;
			this.StartDate = DateTime.Now.ToString("dd / MM / yyyy");
			this.EndDate = DateTime.Now.AddDays(15).ToString("dd / MM / yyyy"); ;
		}


		public void AddOrderDetail(OrderDetail orderDetail)
		{
			OrderDetails.Add(orderDetail);
			Console.WriteLine("Add Order Detail successful");
		}

		private string PrintOrderDetails()
		{
			string a = "";
			foreach (var item in OrderDetails)
			{
				a += item.ToString();
			}
			return a;
		}
		public override string ToString()
		{
			return $"ID Order: {IdOrder} -- Customer: {Customer.Name} -- Date Start: {StartDate} -- Date End: {EndDate}\n" +
				$" {PrintOrderDetails()} \n";
		}

	}

	class OrderDetail
	{
		private static int m_Counter = 1;
		int idOrderDetail;
		Order order;
		BookItem bookItem;

		public int IdOrderDetail { get => idOrderDetail; }
		internal Order Order { get => order; set => order = value; }
		internal BookItem BookItem { get => bookItem; set => bookItem = value; }

		public OrderDetail(Order order, BookItem bookitem)
		{
			this.idOrderDetail = m_Counter++;
			this.Order = order;
			this.BookItem = bookitem;
		}

		
	
		public override string ToString()
		{
			return $"ID Order Detail: {IdOrderDetail} -- Name Book: {BookItem.NameBook} -- Author: {BookItem.Author} \n";

		}


	}
	class BookItem
	{
		private static int m_Counter = 1;
		Admin admin;
		int idBookItem;
		string nameBook;
		string author;
		bool isAvailible;
		private List<OrderDetail> orderDetails = new List<OrderDetail>();

		public Admin Admin { get => admin; set => admin = value; }
		public int IdBookItem { get => idBookItem; }
		public string NameBook { get => nameBook; set => nameBook = value; }
		public string Author { get => author; set => author = value; }
		public bool IsAvailible { get => isAvailible; set => isAvailible = value; }
		

		internal List<OrderDetail> OrderDetails { get => orderDetails; set => orderDetails = value; }

		public BookItem(string nameBook, string author)
		{
			this.idBookItem = m_Counter++;
			this.NameBook = nameBook;
			this.Author = author;
			this.IsAvailible = true;
		}

		public void AddOrderDetail(OrderDetail orderDetail)
		{
			OrderDetails.Add(orderDetail);
		}


		public override string ToString()
		{
			return $"ID Book: {IdBookItem} -- Name Book: {NameBook} -- Author: {Author}-- Is Availible: {IsAvailible} \n";
		}
		public bool RemoveOrderDetail(Order order)
		{
			var OrderDetail = OrderDetails.FirstOrDefault(a => a.Order.Equals(order));
			if (OrderDetails == null)
			{
				return false;
			}
			else
			{
				OrderDetails.Remove(OrderDetail);
				return true;
			}
		}
	}

	class Admin
	{
		int idAdmin;
		List<BookItem> listBooks = new List<BookItem>();
		List<Customer> listCustomers = new List<Customer>();

		public int IdAdmin { get => idAdmin; set => idAdmin = value; }
		internal List<BookItem> ListBooks { get => listBooks; set => listBooks = value; }
		internal List<Customer> ListCustomers { get => listCustomers; set => listCustomers = value; }

		public Admin(int idAdmin)
		{
			this.IdAdmin = idAdmin;
		}

		public void AddBook(BookItem bookItem)
		{
			ListBooks.Add(bookItem);
			Console.WriteLine("Add book successful");
			Console.WriteLine(" ");
		}

		public void UpdateBook(int updateIdBook, string updateNameBook, string updateAuthorBook)
		{
			var Book = ListBooks.FirstOrDefault(a => a.IdBookItem.Equals(updateIdBook));
			if (Book == null)
			{
				AddBook(new BookItem( updateNameBook, updateAuthorBook));
				Console.WriteLine("Add Book Success");
			}
			else
			{
				Book.NameBook = updateNameBook;
				Book.Author = updateAuthorBook;
				Console.WriteLine("Update Book Success");
				Console.WriteLine(" ");
			}
		}

		public bool RemoveBook(int removeIdBook)
		{
			var Book = ListBooks.FirstOrDefault(a => a.IdBookItem.Equals(removeIdBook));
			if (Book == null)
			{
				return false;
			}
			else
			{
				ListBooks.Remove(Book);
				Console.WriteLine("Remove Book Success");
				return true;
			}
		}

		public void ViewBooks()
		{
			foreach (var item in ListBooks)
			{
				Console.WriteLine(item.ToString());
			}
		}

		public Customer SearchCustomer(int id)
		{
			var Customer = ListCustomers.FirstOrDefault(a => a.IdCustomer.Equals(id));
			return Customer;
		}

		public BookItem SearchBook(int id)
		{
			var Book = ListBooks.FirstOrDefault(a => a.IdBookItem.Equals(id));
			return Book;
		}

		public void AddCustomer(Customer customer)
		{
			ListCustomers.Add(customer);
			Console.WriteLine("Add customer successful");
			Console.WriteLine(" ");
		}

		public void UpdateCustomer(Admin admin, int updateIdCustomer, string updateNameCustomer, string updateAddressCustomer, string updateNumberPhone)
		{
			var Customer = ListCustomers.FirstOrDefault(a => a.IdCustomer.Equals(updateIdCustomer));
			if (Customer == null)
			{
				AddCustomer(new Customer(admin, updateNameCustomer, updateAddressCustomer, updateNumberPhone));
				Console.WriteLine("Add Customer Success");
			}
			else
			{
				Customer.Name = updateNameCustomer;
				Customer.Address = updateAddressCustomer;
				Customer.NumberPhone = updateNumberPhone;
				Console.WriteLine("Update Customer Success");
				Console.WriteLine(" ");
			}
		}

		public bool RemoveCustomer(int removeIdCustomer)
		{
			var Customer = ListCustomers.FirstOrDefault(a => a.IdCustomer.Equals(removeIdCustomer));
			if (Customer == null)
			{
				return false;
			}
			else
			{
				ListCustomers.Remove(Customer);
				Console.WriteLine("Remove Customer Success");
				return true;
			}
		}

		public string ViewCustomers()
		{
			string a = "";
			foreach (var item in ListCustomers)
			{
				a += item.ToString();
			}
			return a;
		}
		public override string ToString()
		{
			return $"{IdAdmin}  \n {ViewCustomers()}";
		}
	}

	class PrintAndEnter
	{
		public static int EnterIDOrder()
		{
			Console.Write("Please enter ID Order: ");
			return int.Parse(Console.ReadLine());
		}
		public static int EnterIDOrderDetail()
		{
			Console.Write("Please enter ID Order detail: ");
			return int.Parse(Console.ReadLine());
		}

		public static int EnterIDBook()
		{
			Console.Write("Please enter ID Book: ");
			return int.Parse(Console.ReadLine());
		}
		public static string EnterNameBook()
		{
			Console.Write("Please enter name Book: ");
			return Console.ReadLine();
		}

		public static string EnterAuthorBook()
        {
            Console.Write("Please enter name Author: ");
			return Console.ReadLine();
        }

		public static int EnterIdOrder()
		{
			Console.Write($"Please enter Id Order: ");
			return int.Parse(Console.ReadLine());
		}
		public static string EnterNameCustomer()
		{
			Console.Write("Please enter Name Customer: ");
			return Console.ReadLine();
		}
		public static string EnterPhoneNumberCustomer()
		{
			Console.Write("Please enter Phone Number Customer: ");
			return Console.ReadLine();
		}
		public static string EnterAddressCustomer()
		{
			Console.Write("Please enter Address Customer: ");
			return Console.ReadLine();
		}
		public static int EnterIdCustomer()
		{
			Console.Write("Please enter Id Customer: ");
			return int.Parse(Console.ReadLine());
		}
		public static void PrintMenu()
		{
			Console.WriteLine("***** MENU *****");
			Console.WriteLine("1: Manager Book ");
			Console.WriteLine("2: Manager Customer");
			Console.WriteLine("3: Manager Bill");
			Console.WriteLine("4: Exit!!!");
			Console.WriteLine("5: Clear Screen!!!");

		}
		public static void PrintMenuProduct()
		{
			Console.Clear();
			Console.WriteLine("***** MENU *****");
			Console.WriteLine("1: Add Product ");
			Console.WriteLine("2: Update Product");
			Console.WriteLine("3: Delete Product");
			Console.WriteLine("4: View Product");
			Console.WriteLine("5: Exit!!!");

		}
		public static void PrintMenuCustomer()
		{
			Console.Clear();
			Console.WriteLine("***** MENU *****");
			Console.WriteLine("1: Add Customer ");
			Console.WriteLine("2: Update Customer");
			Console.WriteLine("3: Delete Customer");
			Console.WriteLine("4: View Customer");
			Console.WriteLine("5: Exit!!!");

		}
		public static void PrintMenuOrder()
		{
			Console.Clear();
			Console.WriteLine("***** MENU *****");
			Console.WriteLine("1: Add Order ");
			Console.WriteLine("2: Delete Order");
			Console.WriteLine("3: View Order");
			Console.WriteLine("4: Exit!!!");

		}

		public static string Option()
		{
			Console.Write("Please enter select menu:");
			return Console.ReadLine();
		}
		public static int Option2()
		{
			Console.Write("Please enter select menu:");
			return int.Parse(Console.ReadLine());
		}
	}
	class Command
	{
		public const string INIT = "";
		public const string ManagerProduct = "1";
		public const string ManagerCustomer = "2";
		public const string ManagerOrder = "3";
		public const string Exit = "4";
		public const string ClearScreen = "5";

	}


	class Program
	{
		static void Main(string[] args)
		{
			Admin admin = new Admin(1);
			string option = Command.INIT;
			while (true)
			{
				PrintAndEnter.PrintMenu();
				option = PrintAndEnter.Option();
				switch (option)
				{
					case Command.ManagerProduct:
						PrintAndEnter.PrintMenuProduct();
						int optionProduct = PrintAndEnter.Option2();
						switch (optionProduct)
						{
							case 1:
								Console.WriteLine("How many books do you want to import ? ");
								int n = int.Parse(Console.ReadLine());
								for (int i = 0; i < n; i++)
								{
									try
									{
										admin.AddBook(new BookItem(
										PrintAndEnter.EnterNameBook(),
										PrintAndEnter.EnterAuthorBook()));

									}
									catch (FormatException ex)
									{
										Console.WriteLine("Please enter the correct data type \n" + ex.Message);
									}
									catch (Exception ex)
									{
										Console.WriteLine("Error " + ex.Message);
									}
								}
								break;

							case 2:
								admin.ViewBooks();
								Console.WriteLine("Update Book...");
								try
								{
									admin.UpdateBook(PrintAndEnter.EnterIdCustomer(),
										PrintAndEnter.EnterNameBook(), PrintAndEnter.EnterAuthorBook());
								}
								catch (FormatException ex)
								{
									Console.WriteLine("Please enter the correct data type.\n" + ex.Message);
								}
								catch (Exception ex)
								{
									Console.WriteLine("Error " + ex.Message);
								}
								break;
							case 3:
								admin.ViewBooks();
								Console.WriteLine("Delete Book...");
								try
								{
									admin.RemoveBook(PrintAndEnter.EnterIDBook());
								}
								catch (FormatException ex)
								{
									Console.WriteLine("Please enter the correct data type.\n" + ex.Message);
								}
								catch (Exception ex)
								{
									Console.WriteLine("Error " + ex.Message);
								}
								break;
							case 4:
								admin.ViewBooks();
								string a = Console.ReadLine();
								break;
							case 5:
								break;
							default:
								Console.WriteLine("Please re-enter the selection menu");
								break;
						}
						break;
					case Command.ManagerCustomer:
						PrintAndEnter.PrintMenuCustomer();
						int optionCustomer = PrintAndEnter.Option2();
						switch (optionCustomer)
						{
							case 1:
								Console.WriteLine("How many Customer do you want to import ? ");
								int n = int.Parse(Console.ReadLine());
								for (int i = 0; i < n; i++)
								{
									admin.AddCustomer(new Customer(admin,
										PrintAndEnter.EnterNameCustomer(),
										PrintAndEnter.EnterAddressCustomer(),
										PrintAndEnter.EnterPhoneNumberCustomer()));
								}
								break;

							case 2:
								Console.WriteLine("Update Customer...");
								admin.UpdateCustomer(admin,
									PrintAndEnter.EnterIdCustomer(),
										PrintAndEnter.EnterNameCustomer(),
										PrintAndEnter.EnterAddressCustomer(),
										PrintAndEnter.EnterPhoneNumberCustomer());
								break;
							case 3:
								Console.WriteLine("Delete Customer...");
								admin.RemoveCustomer(PrintAndEnter.EnterIdCustomer());

								break;
							case 4:
								Console.WriteLine(admin.ViewCustomers());
								string b = Console.ReadLine();
								break;
							case 5:
								break;
							default:
								Console.WriteLine("Please re-enter the selection menu");
								break;
						}
						break;
					case Command.ManagerOrder:
						PrintAndEnter.PrintMenuOrder();
						int optionOrder = PrintAndEnter.Option2();
						switch (optionOrder)
						{
							case 1:
								admin.ViewBooks();
								Console.WriteLine(admin.ViewCustomers());
								Console.WriteLine("Please Chose Customer?");
								int id = int.Parse(Console.ReadLine());
								var customerAddOrder = admin.SearchCustomer(id);

								Order order = new Order( customerAddOrder);
								customerAddOrder.AddOrder(order);
								Console.WriteLine("How many order details");
								int numDetail = int.Parse(Console.ReadLine());
								for (int i = 0; i < numDetail; i++)
								{
									var book = admin.SearchBook(PrintAndEnter.EnterIDBook());

									if (book.IsAvailible == true)
									{
										OrderDetail orderDetail = new OrderDetail(
											order, book);
										book.AddOrderDetail(orderDetail);
										order.AddOrderDetail(orderDetail);
										book.IsAvailible = false;
									}
									else Console.WriteLine($"Book {book.NameBook} is not availible");
								}
								break;
							case 2:
								Console.WriteLine("Whose order do you want to delete?");
								int idCustomer = int.Parse(Console.ReadLine());
								var customerDeleteOrder = admin.SearchCustomer(idCustomer);
								Console.WriteLine(customerDeleteOrder.ToString());
								Console.WriteLine("What order do you want to delete?");
								int idOrder = int.Parse(Console.ReadLine());

								var orderb = customerDeleteOrder.SearchOrder(idOrder);
								foreach (var item in orderb.OrderDetails)
								{
									item.BookItem.RemoveOrderDetail(orderb);
									item.BookItem.IsAvailible = true;
								}
								customerDeleteOrder.RemoveOrder(orderb);
								break;
							case 3:
								foreach (var item in admin.ListCustomers)
								{
									Console.WriteLine(item.ToString());
								}
								break;
							default:
								Console.WriteLine("Please re-enter the selection menu");
								break;
						}
						break;
					case Command.Exit:
						break;
					case Command.ClearScreen:
						Console.Clear();
						break;
					default:
						Console.WriteLine("Please re-enter the selection menu");
						break;
				}
			}
		}
	}
}
