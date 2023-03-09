using examen.Models;
using Xunit;

namespace examen.test
{
	public class BeerTests
	{
		[Fact]
		public void TestConnectionToDatabase()
		{
			// Arrange
			var expected = true;

			// Act
			var result = Beer.LoadBeers().Result.Count > 0;

			// Assert
			Assert.Equal(expected, result);
		}
	}
}