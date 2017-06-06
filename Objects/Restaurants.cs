using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace Restaurant
{
  public class Restaurant
  {
    private string _name;
    private int _id;
    private int _foodId;
    // List<FoodType> FoodType = new List<FoodType>(){};

    public Restaurant(string name, int foodId, int id = 0)
    {
      _id = id;
      _name = name;
      _foodId = foodId;
    }

    public string GetName()
    {
      return _name;
    }

    public int GetId()
    {
      return _id;
    }

    public int GetFoodId()
    {
      return _foodId;
    }

    public void SetName(string name)
    {
      _name = name;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO restaurants (name, foodId) OUTPUT INSERTED.id VALUES (@RestaurantName, @FoodType);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@RestaurantName";
      nameParameter.Value = this.GetName();
      cmd.Parameters.Add(nameParameter);
      SqlParameter foodIdParameter = new SqlParameter();
      foodIdParameter.ParameterName = "@FoodType";
      foodIdParameter.Value = this.GetFoodId();
      cmd.Parameters.Add(foodIdParameter);

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

    public static List<Restaurant> GetAllRestaurants()
    {
      List<Restaurant> allRestaurants = new List<Restaurant>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT id, name FROM restaurants;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string restaurantName = rdr.GetString(1);
        Restaurant newRestaurant = new Restaurant(restaurantName, id);
        allRestaurants.Add(newRestaurant);
      }

      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }

      return allRestaurants;
    }

    public override bool Equals(System.Object otherRestaurant)
    {
      if (!(otherRestaurant is Restaurant)) {
        return false;
      }
      else
      {
        Restaurant newRestaurant = (Restaurant) otherRestaurant;
        bool idEquality = (this.GetId() == newRestaurant.GetId());
        bool nameEquality = (this.GetName() == newRestaurant.GetName());

        return (idEquality && nameEquality);
      }
    }

    public static List<Restaurant> GetFoodType()
    {
      List<Restaurant> allRestaurants = new List<Restaurant>{};
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT (id, name, typeOfFood_id) FROM restaurants WHERE typeOfFood_id = 1;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string restaurantName = rdr.GetString(1);
        int foodId = rdr.GetInt32(2);
        Restaurant newRestaurant = new Restaurant(restaurantName, id, foodId);
        allRestaurants.Add(newRestaurant);
      }

      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }

      return allRestaurants;
    }





    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM restaurants;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
  }
}
