﻿using NUnit.Framework;

namespace NW.UnivariateForecasting.UnitTests
{
    [TestFixture]
    public class SlidingWindowItemTests
    {

        // Fields
        private static TestCaseData[] toStringTestCases =
        {

            new TestCaseData(
                MemberRepository.NewSlidingWindowItem,
                MemberRepository.NewSlidingWindowItem_ToString
                ),
            new TestCaseData(
                MemberRepository.SlidingWindow1_Item1,
                MemberRepository.SlidingWindow1_Item1_ToString
                )

        };

        // SetUp
        // Tests
        [TestCaseSource(nameof(toStringTestCases))]
        public void ToString_ShouldReturnExpectedString_WhenInvoked
            (SlidingWindowItem slidingWindowItem, string expected)
        {

            // Arrange
            // Act
            string actual = slidingWindowItem.ToString();

            // Assert
            Assert.AreEqual(expected, actual);

        }

        // TearDown
        // Support methods

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 20.09.2020

*/