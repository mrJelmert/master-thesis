﻿using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RugJelmertModelingLogic;
using RugJelmertModelingLogic.Model;
using RugJelmertModelingLogic.Model.Measurements;

namespace RugJelmertModelingLogicTest
{
    /// <summary>
    /// Summary description for AgentTest
    /// </summary>
    [TestClass]
    public class AgentTest
    {        
        [TestMethod]
        public void TestAgentWeigthExtreme()
        {
            double[] o_i = { 1, 1, 1, 1 };
            double[] o_j = { -1, -1, -1, -1 };
            Agent i = new Agent(o_i, new double[] { 1.0 });
            Agent j = new Agent(o_j, new double[] { -1.0 });

            double weight = i.weigth(j);

            Assert.AreEqual(-1, weight, "The weight calculation isn't correct");
        }

        [TestMethod]
        public void TestAgentWeigth()
        {
            Agent i = new Agent(new double[] { -1 }, new double[] { -1.0 });
            Agent j = new Agent(new double[] { 1 }, new double[] { 1.0 });

            double weight = i.weigth(j);

            Assert.AreEqual(-1, weight, "The weight calculation isn't correct");
        }

        [TestMethod]
        public void TestAgentWeigthDefault()
        {
            double[] o_i = { 1, 1, 1, 1 };

            Agent i = new Agent(o_i, new double[] { 1.0 });

            double weight = i.weigth(i);

            Assert.AreEqual(1, weight, "The weight calculation isn't correct");
        }

        [TestMethod]
        public void TestRandomDistribution()
        {
            Random r = new Random();

            Dictionary<double, int> results = new Dictionary<double, int>();

            //results.Add(1, 0);
            int max = 1000;

            for (int i = 0; i < max; i++)
            {
                double next = r.NextDouble() * (0.95 - -0.95) + -0.95;
                double roundedNext = Math.Round(next, 1);

                if (results.ContainsKey(roundedNext))
                    results[roundedNext] += 1;
                else
                    results.Add(roundedNext, 1);
            }

            int expected = max / results.Count;

            double x2 = 0.0;

            foreach (KeyValuePair<double, int> entry in results)
            {
                double part = (Math.Pow(entry.Value - expected, 2) / expected);
                x2 += part;
            }

            int df = results.Count - 1;

            Assert.IsTrue(x2 < 25, "Not very significant... xi = " + x2);
        }


        [TestMethod]
        public void TestRandomGeneratedFlexibleAttribute()
        {
            AgentBasedModel abm = new AgentBasedModel();

            Agent.numFlex = 1;
            Assert.AreEqual(1, Agent.randomFlexible(abm).Length, "Expected length doesn't match");

            Agent.numFlex = 2;
            Assert.AreEqual(2, Agent.randomFlexible(abm).Length, "Expected length doesn't match");

            Agent.numFlex = 3;
            Assert.AreEqual(3, Agent.randomFlexible(abm).Length, "Expected length doesn't match");

            Agent.numFlex = 40;
            Assert.AreEqual(40, Agent.randomFlexible(abm).Length, "Expected length doesn't match");
        }
    }
}
