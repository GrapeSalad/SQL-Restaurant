using Xunit;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace Restaurant
{
  public class RestaurantTest : IDisposable
  {
    public RestaurantTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=local_restaurants;Integrated Security=SSPI;";
    }

    public void Dispose()
    {
      Restaurant.DeleteAll();
    }

    [Fact]
    public void Test_DatabaseEpmtyAtFirst()
    {
      int resultRestaurant = Restaurant.GetAllRestaurants().Count;
      int resultTypeOfFood = FoodType.GetAllFoodTypes().Count;
      int result = resultTypeOfFood + resultTypeOfFood;
      Assert.Equal(0, result);

    }

    [Fact]
    public void Test_Equal_ReturnsTrueIfDescriptionsAreTheSame()
    {
      Restaurant firstRestaurant = new Restaurant("LittleBird", 1);
      Restaurant secondRestaurant = new Restaurant("LittleBird", 1);

      Assert.Equal(firstRestaurant, secondRestaurant);
    }

    [Fact]
    public void Test_Save_Method_ObjectId()
    {
      Restaurant newRestaurant = new Restaurant("Higgins", 1);
      newRestaurant.Save();
      Restaurant savedRestaurants = Restaurant.GetAllRestaurants()[0];

      int result = savedRestaurants.GetId();
      int testId = newRestaurant.GetId();
      Assert.Equal(testId, result);
    }

    [Fact]
    public void Test_Get_All_Restaurants()
    {
      Restaurant firstRestaurant = new Restaurant("Home", 1);
      firstRestaurant.Save();
      Restaurant secondRestaurant = new Restaurant("LittleBird", 2);
      secondRestaurant.Save();
      Restaurant thirdRestaurant = new Restaurant("TacoBell", 1);
      thirdRestaurant.Save();

      List<Restaurant> testRestaurant = new List<Restaurant> {firstRestaurant, secondRestaurant, thirdRestaurant};
      List<Restaurant> resultsList = Restaurant.GetAllRestaurants();

      Assert.Equal(testRestaurant, resultsList);
    }
    [Fact]
    public void TEST_FoodType_Of_Restaurants()
    {
      Restaurant testRestaurantFoodType = new Restaurant("Home", 1);
      testRestaurantFoodType.Save();
      FoodType testFoodType = new FoodType("homestyle");
      testFoodType.Save();



      Assert.Equal(testFoodType, testRestaurantFoodType.GetFoodType());

    }
  }
}
