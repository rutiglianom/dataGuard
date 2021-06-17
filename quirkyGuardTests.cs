// Matthew Rutigliano
// 4 June 2021
// Revision History: All functions written 6/2/21

using Microsoft.VisualStudio.TestTools.UnitTesting;
using P5;

namespace quirkyGuardTest
{
    [TestClass]
    public class quirkyGuardTests
    {
        [TestMethod]
        public void createActiveObject()
        {
            char[] arr = new char[] { 'a', 'b', 'c', '/' };
            quirkyGuard data = new quirkyGuard(arr);
            Assert.IsTrue(data.State, "quirkyGuard is created inactive with a valid parameter");
        }

        [TestMethod]
        public void createInactiveObject()
        {
            char[] badArr = new char[] { 'a', 'b', 'c' };
            quirkyGuard data = new quirkyGuard(badArr);
            Assert.IsFalse(data.State, " quirkyGuard is created active with an invalid parameter");
        }

        [TestMethod]
        public void valueLength()
        {
            char[] arr = new char[] { 'a', 'b', 'c', '/' };
            int retrieveLength = 2;
            quirkyGuard data = new quirkyGuard(arr);
            char[] result = data.value(retrieveLength);

            Assert.AreEqual(retrieveLength + 1, result.Length, "Value array is of incorrect length");
        }

        [TestMethod]
        public void valueContentsUp()
        {
            char[] arr = new char[] { 'a', 'b', 'c', '/' };
            char[] correctResult = new char[] { 'A', 'b', 'C', '+' };
            quirkyGuard data = new quirkyGuard(arr);
            char[] result = data.value(correctResult.Length-1);

            bool isEqual = true;
            for (int i = 0; i < result.Length; i++)
            {
                isEqual = (result[i] == correctResult[i]);
                if (!isEqual)
                    break;
            }
            Assert.IsTrue(isEqual, "Value array contains incorrect values in up mode");
        }

        [TestMethod]
        public void valueContentsDown()
        {
            int TEST_LENGTH = 100;
            char[] arr = new char[] { 'a', 'b', 'c', '/' };
            char[] correctResult = new char[] { 'a', 'B', 'c', '/' };
            int retrieveLength = correctResult.Length-1;
            quirkyGuard data = new quirkyGuard(arr);
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
            quirkyGuard data = new quirkyGuard(arr);
            char[] result = data.value(-5);
            Assert.AreEqual(result, null, "Invalid value doesn't return null");
        }

        [TestMethod]
        public void modeSwitch()
        {
            int TEST_LENGTH = 100;
            char[] arr = new char[] { 'a', 'b', 'c', '/' };
            int retrieveLength = 2;
            quirkyGuard data = new quirkyGuard(arr);

            int x = 0;
            while (data.Mode & (x++ < TEST_LENGTH))
                data.value(retrieveLength);
            bool prevMode = !data.Mode;
            if (!data.Mode)
            {
                int modeChange = x;
                x = 0;
                while(x++ < TEST_LENGTH && prevMode != data.Mode)
                {
                    prevMode = data.Mode;
                    for (int i = 0; i < modeChange; i++)
                        data.value(retrieveLength);
                }    
            }
            Assert.AreEqual(prevMode, data.Mode, "quirkyGuard doesn't limit amount of mode changes");
        }
    }
}
