using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using XamarinwebApi.Models;

namespace XamarinwebApi.Controllers
{
    public class MastersController : ApiController
    {
        SqlConnection conn;

        private void connection()
        {
            string conString = ConfigurationManager.ConnectionStrings["getConnection"].ToString();
            conn = new SqlConnection(conString);
        }
        public IEnumerable<Employee> GetEmployees()
        {
            List<Employee> employeeData = new List<Employee>();

            connection();
            SqlCommand cmd = new SqlCommand("spGetEmployee", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                Employee employee = new Employee();
                employee.Id = Convert.ToInt32( reader["Id"]);
                employee.Name = reader["Name"].ToString();
                employee.Addresss = reader["Address"].ToString();
                employee.PhoneNumber = reader["PhoneNumber"].ToString();

                employeeData.Add(employee);
            }
            conn.Close();
            return employeeData;
        }
        public Response SaveEmployee(Employee employee)
        {
            Response response = new Response();
            try
            {
                if(string.IsNullOrEmpty(employee.Name))
                {
                    response.Message = "Employee name is mandatory";
                    response.Status = 0;
                }
                else if(string.IsNullOrEmpty(employee.Addresss))
                {
                    response.Message = "Employee address is mandatory";
                    response.Status = 0;
                }
                else if (string.IsNullOrEmpty(employee.PhoneNumber))
                {
                    response.Message = "Employee phone number is mandatory";
                    response.Status = 0;
                }
                else
                {
                    connection();
                    SqlCommand com = new SqlCommand("spAddEmployee", conn);
                    com.CommandType = CommandType.StoredProcedure;

                    com.Parameters.AddWithValue("@Id", employee.Id);
                    com.Parameters.AddWithValue("@Name", employee.Name);
                    com.Parameters.AddWithValue("@Address", employee.Addresss);
                    com.Parameters.AddWithValue("@PhoneNumber", employee.PhoneNumber);

                    conn.Open();
                    int i = com.ExecuteNonQuery();
                    conn.Close();
                    if (i >= 1)
                    {
                        response.Message = "Employee Saved Successfully";
                        response.Status = 1;
                    }
                    else
                    {
                        response.Message = "Employee faild To Save";
                        response.Status = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = 0;
            }
            return response;
        }

        public Response DeleteEmployee(int EmployeeId)
        {
            Response response = new Response();
            try
            {
                connection();
                SqlCommand com = new SqlCommand("spDeleteEmployee", conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                conn.Open();
                int i = com.ExecuteNonQuery();
                conn.Close();
                if (i >= 1)
                {
                    response.Message = "Employee Deleted Successfully";
                    response.Status = 1;
                }
                else
                {
                    response.Message = "Employee faild To Delete";
                    response.Status = 0;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = 0;
            }
            return response;
        }
    }
}
