/*
 * @author:    Kaleb Eberhart
 * @date:      04/20/2018
 * @course:    CST-247
 * @professor: Mark Reha
 * @project:   Benchmark v.1
 * @file:      IBenchLogger.cs
 * @revision:  1
 * @synapsis:  This is the interface for the BenchLogger. These methods must be used in the BenchLogger.
 *             Not much to comment about here since this is just a contract.
 */

namespace Benchmark.Services.Utility
{
    public interface IBenchLogger
    {
        void Debug(string message, string arg = null);

        void Info(string message, string arg = null);

        void Warning(string message, string arg = null);

        void Error(string message, string arg = null);
    }
}
