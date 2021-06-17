// Matthew Rutigliano
// 4 June 2021
// Revision History: All functions written 6/3/21

using Microsoft.VisualStudio.TestTools.UnitTesting;
using P5;

namespace dataGuardTests
{
    [TestClass]
    public class dataGuardTests
    {
        [TestMethod]
        public void createActiveDataLineObject()
        {
            int[] arr = new int[] { 1, 2, 3, 4, 5 };
            dataGuard data = new dataGuard("dataLine", arr);
            Assert.IsTrue(data.getDataLineState(), "Dataline is created inactive with a valid parameter");
        }

        [TestMethod]
        public void createInactiveDataLineObject()
        {
            int[] badArr = null;
            dataGuard data = new dataGuard("dataLine", badArr);
            Assert.IsFalse(data.getDataLineState(), "Dataline is created active with an invalid parameter");
        }

        [TestMethod]
        public void queryTrue()
        {
            int[] arr = new int[] { 1, 2, 3, 4, 5 };
            dataGuard data = new dataGuard("dataLine", arr);
            Assert.IsTrue(data.query(3), "Query cannot find value in Dataline");
        }

        [TestMethod]
        public void queryFalse()
        {
            int[] arr = new int[] { 1, 2, 3, 4, 5 };
            dataGuard data = new dataGuard("dataLine", arr);
            Assert.IsFalse(data.query(20), "Query finds nonexistent value in Dataline");
        }

        [TestMethod]
        public void retrieveLength()
        {
            int[] arr = new int[] { 1, 2, 3, 4, 5 };
            int retrieveLength = 3;
            dataGuard data = new dataGuard("dataLine", arr);
            int[] result = data.retrieve(retrieveLength);

            Assert.AreEqual(retrieveLength, result.Length, "Retrieve array is of incorrect length");
        }

        [TestMethod]
        public void retrieveContents()
        {
            int[] arr = new int[] { 1, 2, 3, 4, 5 };
            int[] correctResult = new int[] { 1, 2, 3 };
            dataGuard data = new dataGuard("dataLine", arr);
            int[] result = data.retrieve(3);

            bool isEqual = true;
            for (int i = 0; i < result.Length; i++)
            {
                isEqual = (result[i] == correctResult[i]);
                if (!isEqual)
                    break;
            }
            Assert.IsTrue(isEqual, "Retrieve array contains incorrect values");
        }

        [TestMethod]
        public void invalidRetrieve()
        {
            int[] arr = new int[] { 1, 2, 3, 4, 5 };
            dataGuard data = new dataGuard("dataLine", arr);
            int[] result = data.retrieve(-5);
            Assert.IsFalse(data.getDataLineState(), "Invalid retrieve doesn't deactivate Dataline");
        }

        [TestMethod]
        public void reset()
        {
            int[] arr = new int[] { 1, 2, 3, 4, 5 };
            dataGuard data = new dataGuard("dataLine", arr);
            int[] result = data.retrieve(-5);
            data.dataLineReset();
            Assert.IsTrue(data.getDataLineState(), "Reset doesn't reactivate Dataline");
        }

        [TestMethod]
        public void mirrorRetrieveContents()
        {
            int[] arr = new int[] { 1, 2, 3, 4, 5 };
            int[] correctResult = new int[] { 1, 2, 3, 4, 5, 5, 4, 3, 2, 1 };
            dataGuard data = new dataGuard("dataMirror", arr);
            int[] result = data.retrieve(10);

            bool isEqual = true;
            for (int i = 0; i < result.Length; i++)
            {
                isEqual = (result[i] == correctResult[i]);
                if (!isEqual)
                    break;
            }
            Assert.IsTrue(isEqual, "Retrieve array contains incorrect values");
        }

        [TestMethod]
        public void insertRetrieveContents()
        {
            int[] arr = new int[] { 1, 2, 3, 4, 5 };
            int[] correctResult = new int[] { 1, 2, 3 };
            dataGuard data = new dataGuard("dataInsert", arr);
            int[] result = data.retrieve(3);

            bool isEqual = true;
            for (int i = 0; i < result.Length; i++)
            {
                isEqual = (result[i] == correctResult[i]);
                if (!isEqual)
                    break;
            }
            Assert.IsTrue(isEqual, "Retrieve array contains incorrect values");
        }

        [TestMethod]
        public void createActiveGuardObject()
        {
            char[] arr = new char[] { 'a', 'b', 'c', '/' };
            dataGuard data = new dataGuard("", null, "guard", arr);
            Assert.IsTrue(data.getGuardState(), "Guard is created inactive with a valid parameter");
        }

        [TestMethod]
        public void createInactiveGuardObject()
        {
            char[] badArr = new char[] { 'a', 'b', 'c' };
            dataGuard data = new dataGuard("", null, "guard", badArr);
            Assert.IsFalse(data.getGuardState(), "Guard is created active with an invalid parameter");
        }

        [TestMethod]
        public void valueLength()
        {
            char[] arr = new char[] { 'a', 'b', 'c', '/' };
            int retrieveLength = 2;
            dataGuard data = new dataGuard("", null, "guard", arr);
            char[] result = data.value(retrieveLength);

            Assert.AreEqual(retrieveLength, result.Length, "Value array is of incorrect length");
        }

        [TestMethod]
        public void valueContents()
        {
            char[] arr = new char[] { 'a', 'b', 'c', '/' };
            char[] correctResult = new char[] { 'A', 'B' };
            dataGuard data = new dataGuard("", null, "guard", arr);
            char[] result = data.value(2);

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
            int retrieveLength = 2;
            char[] arr = new char[] { 'a', 'b', 'c', '/' };
            char[] correctResult = new char[] { 'a', 'b' };
            dataGuard data = new dataGuard("", null, "guard", arr);
            char[] result = data.value(retrieveLength);

            int x = 0;
            while (data.getGuardMode() & (x < TEST_LENGTH))
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
            dataGuard data = new dataGuard("", null, "guard", arr);
            char[] result = data.value(-5);
            Assert.AreEqual(result, null, "Invalid value doesn't return null");
        }

        [TestMethod]
        public void skipValueContents()
        {
            char[] arr = new char[] { 'a', 'b', 'c', '/' };
            char[] correctResult = new char[] { 'A', 'B', 'C' };
            dataGuard data = new dataGuard("", null, "skipGuard", arr);
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
        public void quirkyValueContentsUp()
        {
            char[] arr = new char[] { 'a', 'b', 'c', '/' };
            char[] correctResult = new char[] { 'A', 'b', 'C', '+' };
            dataGuard data = new dataGuard("", null, "quirkyGuard", arr);
            char[] result = data.value(correctResult.Length - 1);

            bool isEqual = true;
            for (int i = 0; i < result.Length; i++)
            {
                isEqual = (result[i] == correctResult[i]);
                if (!isEqual)
                    break;
            }
            Assert.IsTrue(isEqual, "Value array contains incorrect values in up mode");
        }
    }
}
