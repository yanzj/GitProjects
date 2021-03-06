﻿using Connection;
using Market_Manager.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market_Manager.DataConnection
{
    class Employer_Data
    {
        //Employer log in
        public static EmployerModel Login(string id_employer, string password)
        {
            //It's not done
            return new EmployerModel();
        }

        //Employer information
        public static EmployerModel Employer_Info(string id_employer)
        {
            //It's not done
            return new EmployerModel();
        }

        //Add new employer

        //Update employer infomation

        //Remove employer
    }

    class Customer_Data {
        //add a new customer
        public static void newCustomer(CustomerModel customer)
        {
            string cmd = string.Format("EXEC UpdateCustomer '{0}','{1}','{2}','{3}','{4}','{5}','{6}'"
                , customer.id, customer.name, customer.lastname, customer.phone, customer.address, customer.email, customer.dni);
            Utilities.execute(cmd);
        }

        //Update customer information

        //Remove customer
        public static void removeCustomer(string id_customer)
        {
            string cmd = string.Format("EXEC RemoveCustomer '{0}'", id_customer);
            Utilities.execute(cmd);
        }

        //Customer information
        public static CustomerModel getCustomer(string id_customer)
        {
            string cmd = string.Format("Select * from Customers where id_customer='{0}'", id_customer.Trim());
            DataSet data = Utilities.execute(cmd);
            var id = data.Tables[0].Rows[0]["id_customer"].ToString().Trim();
            var name = data.Tables[0].Rows[0]["customer_name"].ToString().Trim();
            var lastname = data.Tables[0].Rows[0]["customer_lastname"].ToString().Trim();
            var address = data.Tables[0].Rows[0]["customer_address"].ToString().Trim();
            var phone = data.Tables[0].Rows[0]["customer_phone"].ToString().Trim();
            var email = data.Tables[0].Rows[0]["customer_email"].ToString().Trim();
            var dni = data.Tables[0].Rows[0]["customer_dni"].ToString().Trim();
            return new CustomerModel(id,name,lastname,phone,address,email,dni);
        }

        //Get all Customers
        public static List<CustomerModel> getAllCustomers()
        {
            var listCustomer = new List<CustomerModel>();
            string cmd = string.Format("Select * from Customers");
            DataSet data = Utilities.execute(cmd);
            for (int i = 0; i < data.Tables[0].Rows.Count; i++)
            {
                var row = data.Tables[0].Rows[i]; 
                var customer = new CustomerModel(
                    row["id_customer"].ToString().Trim(),
                    row["customer_name"].ToString().Trim(),
                    row["customer_lastname"].ToString().Trim(),
                    row["customer_address"].ToString().Trim(),
                    row["customer_phone"].ToString().Trim(),
                    row["customer_email"].ToString().Trim(),
                    row["customer_dni"].ToString().Trim()
                    );
                listCustomer.Add(customer);
            }
            return listCustomer;
        }
    }

    class Product_Data
    {
        //Update product info
        public static void updateProduct(Product product)
            {
                string cmd = string.Format("EXEC UpdateProduct '{0}','{1}','{2}','{3}','{4}'"
                , product.id, product.name, product.mark, product.price, product.quantity);
                Utilities.execute(cmd);

            }

        //Get product
        public static Product getProduct(string id_product)
        {
            string cmd = string.Format("Select * from Items where id_item='{0}'", id_product);
            DataSet data = Utilities.execute(cmd);
            var id = data.Tables[0].Rows[0]["id_item"].ToString().Trim();
            var name = data.Tables[0].Rows[0]["item_name"].ToString().Trim();
            var mark = data.Tables[0].Rows[0]["item_mark"].ToString().Trim();
            var price =double.Parse(data.Tables[0].Rows[0]["item_price"].ToString().Trim());
            var quantity = int.Parse(data.Tables[0].Rows[0]["item_quantity"].ToString().Trim());
            return new Product(id, name, mark, price, quantity);
        }

        //Get all product in stock
        public static List<Product> getAllProducts()
        {
            var listProducts = new List<Product>();
            string cmd = string.Format("Select * from Items where item_status = 'in stock'");
            DataSet data = Utilities.execute(cmd);
            for (int i = 0; i < data.Tables[0].Rows.Count; i++)
            {
                var row = data.Tables[0].Rows[i];
                var product = new Product(
                    row["id_item"].ToString().Trim(),
                    row["item_name"].ToString().Trim(),
                    row["item_mark"].ToString().Trim(),
                    double.Parse(row["item_price"].ToString().Trim()),
                    int.Parse(row["item_quantity"].ToString().Trim())
                    );
                listProducts.Add(product);
            }
            return listProducts;
        }
       
        
    }

    class Purshase
    {
        //Add new product to a purshase
        public static void AddProduct(string id_purshase, string id_customer, string id_item, int quantity)
        {
            string cmd = string.Format("EXEC AddProduct '{0}','{1}','{2}','{3}'", id_purshase, id_customer, id_item, quantity);
            Utilities.execute(cmd);
        }

        //Get product selected info
        public static Purshase_Product GetProductSelected(string id_purshase, string id_product, string id_customer)
        {
            string cmd = string.Format("EXEC GetProductSelected '{0}','{1}','{2}'", id_purshase,id_product,id_customer);
            DataSet data = Utilities.execute(cmd);

            var id = data.Tables[0].Rows[0]["id_item"].ToString().Trim();
            var name = data.Tables[0].Rows[0]["item_name"].ToString().Trim();
            var mark = data.Tables[0].Rows[0]["item_mark"].ToString().Trim();
            var price = double.Parse(data.Tables[0].Rows[0]["item_price"].ToString().Trim());
            var quantity_prod = int.Parse(data.Tables[0].Rows[0]["item_purshased_quantity"].ToString().Trim());
            var amount = double.Parse(data.Tables[0].Rows[0]["item_purshased_amount"].ToString().Trim());
            var total = double.Parse(data.Tables[0].Rows[0]["purshase_amount"].ToString().Trim());

            return new Purshase_Product(id, name, mark, price, quantity_prod, amount, total);
        }

        //Get Purshase processed
        public static PurshaseInfo GetPurshase(string id_purshase)
        {
            string cmd = string.Format("EXEC GetPurshaseProcessed '{0}'", id_purshase);
            DataSet data = Utilities.execute(cmd);
            List<Purshase_Product> products = new List<Purshase_Product>();
            if (data.Tables[0].Rows.Count != 0)
            {
                var id_customer = data.Tables[0].Rows[0]["id_customer"].ToString().Trim();
                var customer_name = data.Tables[0].Rows[0]["customer_name"].ToString().Trim();
                customer_name += " " + data.Tables[0].Rows[0]["customer_lastname"].ToString().Trim();
                var date = data.Tables[0].Rows[0]["purshase_date"].ToString().Trim();

                for (int i = 0; i < data.Tables[0].Rows.Count; i++)
                {
                    var row = data.Tables[0].Rows[i];
                    var product = new Purshase_Product(
                        row["id_item"].ToString().Trim(),
                        row["item_name"].ToString().Trim(),
                        row["item_mark"].ToString().Trim(),
                        double.Parse(row["item_price"].ToString().Trim()),
                        int.Parse(row["item_purshased_quantity"].ToString().Trim()),
                        double.Parse(row["item_purshased_amount"].ToString().Trim()),
                        double.Parse(row["purshase_amount"].ToString().Trim())
                        );
                    products.Add(product);
                }

                return new PurshaseInfo(id_purshase, id_customer, customer_name, date, products);
            }
            else
                return new PurshaseInfo();            
        }
       
        // Return product processed
        public static void ReturnProduct(string id_purshase, string id_product, int quantity, string state)
        {
            string cmd = string.Format("EXEC ReturnProduct '{0}','{1}', '{2}', '{3}'", id_purshase, id_product, quantity, state);
            Utilities.execute(cmd);
        }        

        //Process a sale
        public static void ProcessSaleOrder(string id_purshase)
        {
            string cmd = string.Format("EXEC ProcessSaleOrder '{0}'", id_purshase);
            Utilities.execute(cmd);
        }

        //Cancel a sale
        public static void CancelSaleOrder(string id_purshase)
        {
            string cmd = string.Format("EXEC CancelSaleOrder '{0}'",id_purshase);
            Utilities.execute(cmd);
        }
    }

    enum Return_State
    {
        damaged, used, good
    }
    
}
