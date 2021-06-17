// Matthew Rutigliano
// 4 June 2021
// Revision History: All functions written 6/2/21

using Microsoft.VisualStudio.TestTools.UnitTesting;
using P5;

namespace skipGuardTests
{
    [TestClass]
    public class skipGuardTests
    {
        [TestMethod]
        public void createActiveObject()
        {
            char[] arr = new char[] { 'a', 'b', 'c', '/' };
            skipGuard data = new skipGuard(arr);
            Assert.IsTrue(data.State, "skipGuard is created inactive with a valid parameter");
        }

        [TestMethod]
        public void createInactiveObject()
        {
            char[] badArr = new char[] { 'a', 'b', 'c' };
            skipGuard data = new skipGuard(badArr);
            Assert.IsFalse(data.State, "skipGuard is created active with an invalid parameter");
        }

        [TestMethod]
        public void valueLength()
        {
            char[] arr = new char[] { 'a', 'b', 'c', '/' };
            int retrieveLength = 3;
            skipGuard data = new skipGuard(arr);
            char[] result = data.value(retrieveLength);

            Assert.AreEqual(retrieveLength, result.Length, "Value array is of incorrect length");
        }

        [TestMethod]
        public void valueContents()
        {
            char[] arr = new char[] { 'a', 'b', 'c', '/' };
            char[] correctResult = new char[] { 'A', 'B', 'C' };
            skipGuard data = new skipGuard(arr);
            char[] result = data.value(correctResult.Length);

            bool isEqual = true;
            for (int i = 0; i < result.Length; i++)
            {
                isEqual = (result[i] == correctResult[i]);
                if (!isEqual)
                    break;
            }
            Assert.IsTrue(isEqual, "Value array contains incorrect values");
        }

        [TestMethod]
        public void valueLowerContents()
        {
            int TEST_LENGTH = 100;
            char[] arr = new char[] { 'a', 'b', 'c', '/' };
            char[] correctResult = new char[] { 'a', 'b', 'c' };
            int retrieveLength = correctResult.Length;
            skipGuard data = new skipGuard(arr);
            char[] result = data.value(retrieveLength);

            int x = 0;
            while (data.Mode & (x < TEST_LENGTH))
            {
                result = data.value(retrieveLength);
                x++;
            }
            result = data.value(retrieveLength);

            bool isEqual = true;
            for (int i = 0; i < result.Length; i++)
            {
                isEqual = (result[i] == correctResult[i]);
                if (!isEqual)
                    break;
            }
            Assert.IsTrue(isEqual, "Value array contents don't change to lowercase");
        }

        [TestMethod]
        public void invalidValue()
        {
            char[] arr = new char[] { 'a', 'b', 'c', '/' };
            skipGuard data = new skipGuard(arr);
            char[] result = data.value(-5);
            Assert.AreEqual(result, null, "Invalid value doesn't return null");
        }

        [TestMethod]
        public void skip()
        {
            char[] arr = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', '/' };
            char[] correctResult = new char[] { 'A', 'B', 'C', 'E' };
            skipGuard data = new skipGuard(arr);
            char[] result = data.value(correctResult.Length);

            bool isEqual = true;
            for (int i = 0; i < result.Length; i++)
            {
                isEqual = (result[i] == correctResult[i]);
                if (!isEqual)
                    break;
            }
            Assert.IsTrue(isEqual, "Value array doesn't skip appropriately");
        }
    }
}
