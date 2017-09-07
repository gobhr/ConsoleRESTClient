using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;


namespace ConsoleRESTClient
{
	class Program
	{
		static void Main(string[] args)
		{
			RunAsync().Wait();
		}

		static async Task RunAsync()
		{
			//Person person = new Person();
			try
			{
				using (var client = new HttpClient())
				{
					//go get the data
					client.BaseAddress = new Uri("http://localhost:41755/");
					client.DefaultRequestHeaders.Accept.Clear();
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

					HttpResponseMessage response;

					Console.WriteLine("Post");
					Person newPerson = new Person();
					newPerson.FName = "Jason";
					newPerson.LName = "Bonham";
					newPerson.PayRate = Convert.ToDecimal("250.00");
					newPerson.StartDate = Convert.ToDateTime("09/15/1990");
					newPerson.EndDate = Convert.ToDateTime("09/15/2999");
					newPerson.EmailAddress = "ahmetErtigun@ledZeppelin.com";

					response = await client.PostAsJsonAsync("api/Person", newPerson);
					if (response.IsSuccessStatusCode)
					{
						Uri personUrl = response.Headers.Location;
						Console.WriteLine(personUrl);

						// put or udpate
						newPerson.PayRate = Convert.ToDecimal("275.00");
						response = await client.PutAsJsonAsync(personUrl, newPerson);

						//delete just added above
						response = await client.DeleteAsync(personUrl);
					}
					Console.WriteLine();

					Console.WriteLine("Get");
					response = await client.GetAsync("api/Person/1002");
					if (response.IsSuccessStatusCode)
					{
						Person person = await response.Content.ReadAsAsync<Person>();
						Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}", person.FName, person.LName, person.PayRate, person.StartDate.ToShortDateString(), person.EmailAddress);
					}
					Console.WriteLine();

					Console.WriteLine("Get All People");
					response = await client.GetAsync("api/Person");
					if (response.IsSuccessStatusCode)
					{
						List<Person> person = await response.Content.ReadAsAsync<List<Person>>();
						for (int i = 0; i < person.Count; i++)
						{
							Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}", person[i].FName, person[i].LName, person[i].PayRate, person[i].StartDate.ToShortDateString(), person[i].EmailAddress);
						}
					}
					Console.ReadLine();
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("{0}",ex.Message);
			}			
		}
	}
}
