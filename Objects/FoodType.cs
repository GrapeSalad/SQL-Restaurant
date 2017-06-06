using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Restaurant
{
  public class FoodType
  {
    private int _id;
    private string _type;
    public static List<string> foodTypes = new List<string>(){};

    public FoodType(string typeOfFood, int id = 0)
    {
      _id = id;
      _type = typeOfFood;
    }

    public string GetType()
    {
      return _type;
    }
    public void SetType(string typeOfFood)
    {
      _type = typeOfFood;
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM typeOfFood;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }

    public static List<FoodType> GetAllFoodTypes()
    {
      List<FoodType> allFoodTypes = new List<FoodType>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT id, type FROM typeOfFood;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string typeOfFood = rdr.GetString(1);
        FoodType newFoodType = new FoodType(typeOfFood, id);
        allFoodTypes.Add(newFoodType);
      }

      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }

      return allFoodTypes;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO typeOfFood (type) OUTPUT INSERTED.id VALUES (@FoodType);", conn);

      SqlParameter typeParameter = new SqlParameter();
      typeParameter.ParameterName = "@FoodType";
      typeParameter.Value = this.GetType();

      cmd.Parameters.Add(typeParameter);

      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }
  }
}
