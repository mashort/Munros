using Munros.Core.Entities;
using Xunit;

namespace Munros.Test
{
    public class QueryParametersShould
    {
        [Fact]
        public void ReturnTrueWhenMaxHeightIsGreaterThanMinHeight()
        {
            QueryParameters queryParameters = new QueryParameters 
            { 
                MinHeight = 930,
                MaxHeight = 1000
            };

            Assert.True(queryParameters.MinMaxHeightValid());
        }

        [Fact]
        public void ReturnFalseWhenMaxHeightIsLessThanMinHeight()
        {
            QueryParameters queryParameters = new QueryParameters
            {
                MinHeight = 930,
                MaxHeight = 900
            };

            Assert.False(queryParameters.MinMaxHeightValid());
        }
    }
}
