/*
 * Copyright (c) 2010-2025, doipc.com Development Team
 *
 * SPDX-License-Identifier: Apache-2.0
 *
 * Change Logs:
 * Date                 Author      Notes
 * 2024-04-03           Andy        the first version
 * 2025-12-31           Andy        Split, restructure
 * 2026-01-02           Andy        Refactored for better extensibility and standardization
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Erp.Toolkit.Controls
{
    #region Enums and Configuration

    /// <summary>
    /// 聚合操作类型
    /// </summary>
    [Serializable]
    public enum DgvAggregatorType
    {
        /// <summary>
        /// 求和
        /// </summary>
        Sum,

        /// <summary>
        /// 平均值
        /// </summary>
        Avg,

        /// <summary>
        /// 最大值
        /// </summary>
        Max,

        /// <summary>
        /// 最小值
        /// </summary>
        Min,

        /// <summary>
        /// 计数
        /// </summary>
        Count,

        /// <summary>
        /// 去重计数
        /// </summary>
        DistinctCount,

        /// <summary>
        /// 乘积
        /// </summary>
        Product,

        /// <summary>
        /// 乘积求和
        /// </summary>
        SumProduct,

        /// <summary>
        /// 方差
        /// </summary>
        Variance,

        /// <summary>
        /// 总体方差
        /// </summary>
        PopulationVariance,

        /// <summary>
        /// 标准差
        /// </summary>
        StandardDeviation,

        /// <summary>
        /// 总体标准差
        /// </summary>
        PopulationStandardDeviation,

        /// <summary>
        /// 中位数
        /// </summary>
        Median,

        /// <summary>
        /// 众数
        /// </summary>
        Mode,

        /// <summary>
        /// 四分位数
        /// </summary>
        Quartile,

        /// <summary>
        /// 百分位数
        /// </summary>
        Percentile,

        /// <summary>
        /// 偏度
        /// </summary>
        Skewness,

        /// <summary>
        /// 峰度
        /// </summary>
        Kurtosis,

        /// <summary>
        /// 变异系数
        /// </summary>
        CoefficientOfVariation,

        /// <summary>
        /// 几何平均数
        /// </summary>
        GeometricMean,

        /// <summary>
        /// 调和平均数
        /// </summary>
        HarmonicMean,

        /// <summary>
        /// 范围（极差）
        /// </summary>
        Range,

        /// <summary>
        /// 绝对值求和
        /// </summary>
        SumAbsolute,

        /// <summary>
        /// 平方和
        /// </summary>
        SumSquares,

        /// <summary>
        /// 均方根
        /// </summary>
        RootMeanSquare,

        /// <summary>
        /// 自定义表达式
        /// </summary>
        Custom
    }

    /// <summary>
    /// 聚合配置
    /// </summary>
    [Serializable]
    public class DgvAggregatorConfig
    {
        /// <summary>
        /// 分组列列表
        /// </summary>
        public List<string> GroupColumns { get; set; } = new List<string>();

        /// <summary>
        /// 聚合列配置列表
        /// </summary>
        public List<DgvAggregatorColumnConfig> AggregateColumns { get; set; } = new List<DgvAggregatorColumnConfig>();
    }

    /// <summary>
    /// 聚合列配置
    /// </summary>
    [Serializable]
    public class DgvAggregatorColumnConfig
    {
        /// <summary>
        /// 源数据列名或表达式
        /// </summary>
        public string SourceColumn { get; set; }

        /// <summary>
        /// 结果列名
        /// </summary>
        public string ResultColumn { get; set; }

        /// <summary>
        /// 聚合类型
        /// </summary>
        public DgvAggregatorType AggregateType { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public Type DataType { get; set; }

        /// <summary>
        /// 自定义表达式（当AggregateType为Custom时使用）
        /// </summary>
        public string CustomExpression { get; set; }

        /// <summary>
        /// 附加参数（用于需要额外参数的统计指标，如百分位数）
        /// </summary>
        public Dictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();
    }

    #endregion Enums and Configuration

    #region Core Interfaces

    /// <summary>
    /// 聚合策略接口
    /// </summary>
    public interface IAggregateStrategy
    {
        /// <summary>
        /// 聚合类型
        /// </summary>
        DgvAggregatorType AggregateType { get; }

        /// <summary>
        /// 计算聚合值
        /// </summary>
        /// <param name="values">数值列表</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="parameters">附加参数</param>
        /// <returns>聚合结果</returns>
        object Calculate(IEnumerable<object> values, Type dataType, Dictionary<string, object> parameters);

        /// <summary>
        /// 获取结果列的数据类型
        /// </summary>
        /// <param name="sourceType">源数据类型</param>
        /// <returns>结果数据类型</returns>
        Type GetResultColumnType(Type sourceType);
    }

    /// <summary>
    /// 聚合策略工厂接口
    /// </summary>
    public interface IAggregateStrategyFactory
    {
        /// <summary>
        /// 创建聚合策略
        /// </summary>
        /// <param name="aggregateType">聚合类型</param>
        /// <returns>聚合策略</returns>
        IAggregateStrategy CreateStrategy(DgvAggregatorType aggregateType);

        /// <summary>
        /// 注册自定义策略
        /// </summary>
        /// <param name="aggregateType">聚合类型</param>
        /// <param name="strategy">策略实例</param>
        void RegisterStrategy(DgvAggregatorType aggregateType, IAggregateStrategy strategy);

        /// <summary>
        /// 获取支持的聚合类型
        /// </summary>
        /// <param name="dataType">数据类型</param>
        /// <returns>支持的聚合类型列表</returns>
        IEnumerable<DgvAggregatorType> GetSupportedAggregatorTypes(Type dataType);
    }

    /// <summary>
    /// 表达式计算器接口
    /// </summary>
    public interface IExpressionCalculator
    {
        /// <summary>
        /// 计算表达式值
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="row">数据行</param>
        /// <returns>计算结果</returns>
        object EvaluateExpression(string expression, DataRow row);

        /// <summary>
        /// 解析乘积表达式
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="row">数据行</param>
        /// <returns>乘积值</returns>
        List<object> ParseProductExpression(string expression, DataRow row);
    }

    #endregion Core Interfaces

    #region Core Implementations

    /// <summary>
    /// 聚合策略工厂
    /// </summary>
    public class AggregateStrategyFactory : IAggregateStrategyFactory
    {
        private readonly Dictionary<DgvAggregatorType, IAggregateStrategy> _strategies;

        public AggregateStrategyFactory()
        {
            _strategies = new Dictionary<DgvAggregatorType, IAggregateStrategy>
            {
                { DgvAggregatorType.Sum, new SumAggregateStrategy() },
                { DgvAggregatorType.Avg, new AverageAggregateStrategy() },
                { DgvAggregatorType.Max, new MaxAggregateStrategy() },
                { DgvAggregatorType.Min, new MinAggregateStrategy() },
                { DgvAggregatorType.Count, new CountAggregateStrategy() },
                { DgvAggregatorType.DistinctCount, new DistinctCountAggregateStrategy() },
                { DgvAggregatorType.Product, new ProductAggregateStrategy() },
                { DgvAggregatorType.SumProduct, new SumProductAggregateStrategy() },
                { DgvAggregatorType.Variance, new VarianceAggregateStrategy() },
                { DgvAggregatorType.PopulationVariance, new PopulationVarianceAggregateStrategy() },
                { DgvAggregatorType.StandardDeviation, new StandardDeviationAggregateStrategy() },
                { DgvAggregatorType.PopulationStandardDeviation, new PopulationStandardDeviationAggregateStrategy() },
                { DgvAggregatorType.Median, new MedianAggregateStrategy() },
                { DgvAggregatorType.Mode, new ModeAggregateStrategy() },
                { DgvAggregatorType.Quartile, new QuartileAggregateStrategy() },
                { DgvAggregatorType.Percentile, new PercentileAggregateStrategy() },
                { DgvAggregatorType.Skewness, new SkewnessAggregateStrategy() },
                { DgvAggregatorType.Kurtosis, new KurtosisAggregateStrategy() },
                { DgvAggregatorType.CoefficientOfVariation, new CoefficientOfVariationAggregateStrategy() },
                { DgvAggregatorType.GeometricMean, new GeometricMeanAggregateStrategy() },
                { DgvAggregatorType.HarmonicMean, new HarmonicMeanAggregateStrategy() },
                { DgvAggregatorType.Range, new RangeAggregateStrategy() },
                { DgvAggregatorType.SumAbsolute, new SumAbsoluteAggregateStrategy() },
                { DgvAggregatorType.SumSquares, new SumSquaresAggregateStrategy() },
                { DgvAggregatorType.RootMeanSquare, new RootMeanSquareAggregateStrategy() },
                { DgvAggregatorType.Custom, new CustomAggregateStrategy() }
            };
        }

        public IAggregateStrategy CreateStrategy(DgvAggregatorType aggregateType)
        {
            if (_strategies.TryGetValue(aggregateType, out var strategy))
            {
                return strategy;
            }

            throw new NotSupportedException($"聚合类型 '{aggregateType}' 不支持");
        }

        public void RegisterStrategy(DgvAggregatorType aggregateType, IAggregateStrategy strategy)
        {
            _strategies[aggregateType] = strategy;
        }

        public IEnumerable<DgvAggregatorType> GetSupportedAggregatorTypes(Type dataType)
        {
            var supportedTypes = new List<DgvAggregatorType>
            {
                DgvAggregatorType.Count,
                DgvAggregatorType.DistinctCount
            };

            if (IsNumericType(dataType))
            {
                supportedTypes.AddRange(new[]
                {
                    DgvAggregatorType.Sum,
                    DgvAggregatorType.Avg,
                    DgvAggregatorType.Max,
                    DgvAggregatorType.Min,
                    DgvAggregatorType.Product,
                    DgvAggregatorType.SumProduct,
                    DgvAggregatorType.Variance,
                    DgvAggregatorType.PopulationVariance,
                    DgvAggregatorType.StandardDeviation,
                    DgvAggregatorType.PopulationStandardDeviation,
                    DgvAggregatorType.Median,
                    DgvAggregatorType.Mode,
                    DgvAggregatorType.Quartile,
                    DgvAggregatorType.Percentile,
                    DgvAggregatorType.Skewness,
                    DgvAggregatorType.Kurtosis,
                    DgvAggregatorType.CoefficientOfVariation,
                    DgvAggregatorType.GeometricMean,
                    DgvAggregatorType.HarmonicMean,
                    DgvAggregatorType.Range,
                    DgvAggregatorType.SumAbsolute,
                    DgvAggregatorType.SumSquares,
                    DgvAggregatorType.RootMeanSquare,
                    DgvAggregatorType.Custom
                });
            }

            return supportedTypes;
        }

        /// <summary>
        /// 判断类型是否为数值类型
        /// </summary>
        private static bool IsNumericType(Type type)
        {
            if (type == null) return false;

            Type underlyingType = Nullable.GetUnderlyingType(type) ?? type;

            return underlyingType == typeof(byte) ||
                   underlyingType == typeof(sbyte) ||
                   underlyingType == typeof(short) ||
                   underlyingType == typeof(ushort) ||
                   underlyingType == typeof(int) ||
                   underlyingType == typeof(uint) ||
                   underlyingType == typeof(long) ||
                   underlyingType == typeof(ulong) ||
                   underlyingType == typeof(float) ||
                   underlyingType == typeof(double) ||
                   underlyingType == typeof(decimal);
        }
    }

    /// <summary>
    /// 表达式计算器
    /// </summary>
    public class ExpressionCalculator : IExpressionCalculator
    {
        public object EvaluateExpression(string expression, DataRow row)
        {
            if (string.IsNullOrEmpty(expression))
                return DBNull.Value;

            try
            {
                // 使用DataTable.Compute计算表达式
                return row.Table.Compute(expression, "");
            }
            catch
            {
                // 如果计算失败，尝试直接返回列值
                if (row.Table.Columns.Contains(expression))
                    return row[expression];

                return DBNull.Value;
            }
        }

        public List<object> ParseProductExpression(string expression, DataRow row)
        {
            List<object> values = new List<object>();

            if (string.IsNullOrEmpty(expression))
                return values;

            // 解析乘法表达式
            string[] parts = expression.Split('*');
            foreach (string part in parts)
            {
                string columnName = part.Trim();
                if (row.Table.Columns.Contains(columnName))
                {
                    object value = row[columnName];
                    if (value != DBNull.Value && value != null)
                    {
                        values.Add(value);
                    }
                }
            }

            return values;
        }
    }

    #endregion Core Implementations

    #region Utility Classes

    /// <summary>
    /// 数学计算辅助类
    /// </summary>
    public static class MathHelper
    {
        /// <summary>
        /// 转换为double类型
        /// </summary>
        public static double ToDouble(object value)
        {
            if (value == null || value == DBNull.Value)
                return double.NaN;

            try
            {
                return Convert.ToDouble(value);
            }
            catch
            {
                return double.NaN;
            }
        }

        /// <summary>
        /// 过滤有效数值
        /// </summary>
        public static IEnumerable<double> FilterValidValues(IEnumerable<object> values)
        {
            foreach (var value in values)
            {
                double d = ToDouble(value);
                if (!double.IsNaN(d))
                {
                    yield return d;
                }
            }
        }

        /// <summary>
        /// 计算偏度（样本偏度）
        /// </summary>
        public static double CalculateSkewness(List<double> values)
        {
            int n = values.Count;
            if (n < 3) return double.NaN;

            double mean = values.Average();
            double sumCubes = 0;
            double sumSquares = 0;

            foreach (double x in values)
            {
                double diff = x - mean;
                sumCubes += diff * diff * diff;
                sumSquares += diff * diff;
            }

            double variance = sumSquares / (n - 1);
            double stdDev = Math.Sqrt(variance);

            if (stdDev == 0) return 0;

            return (sumCubes / n) / Math.Pow(stdDev, 3);
        }

        /// <summary>
        /// 计算峰度（样本峰度）
        /// </summary>
        public static double CalculateKurtosis(List<double> values)
        {
            int n = values.Count;
            if (n < 4) return double.NaN;

            double mean = values.Average();
            double sumQuads = 0;
            double sumSquares = 0;

            foreach (double x in values)
            {
                double diff = x - mean;
                sumQuads += diff * diff * diff * diff;
                sumSquares += diff * diff;
            }

            double variance = sumSquares / (n - 1);

            if (variance == 0) return 0;

            return (sumQuads / n) / (variance * variance) - 3;
        }

        /// <summary>
        /// 计算几何平均数
        /// </summary>
        public static double CalculateGeometricMean(List<double> values)
        {
            if (values.Count == 0) return double.NaN;

            double product = 1;
            int count = 0;

            foreach (double value in values)
            {
                if (value <= 0) continue; // 几何平均数要求所有值为正数
                product *= value;
                count++;
            }

            if (count == 0) return double.NaN;

            return Math.Pow(product, 1.0 / count);
        }

        /// <summary>
        /// 计算调和平均数
        /// </summary>
        public static double CalculateHarmonicMean(List<double> values)
        {
            if (values.Count == 0) return double.NaN;

            double sumReciprocals = 0;
            int count = 0;

            foreach (double value in values)
            {
                if (value == 0) return double.NaN; // 调和平均数不能包含0
                sumReciprocals += 1.0 / value;
                count++;
            }

            if (count == 0) return double.NaN;

            return count / sumReciprocals;
        }

        /// <summary>
        /// 计算四分位数
        /// </summary>
        public static double CalculateQuartile(List<double> sortedValues, int quartile)
        {
            if (sortedValues.Count == 0) return double.NaN;
            if (quartile < 1 || quartile > 3) return double.NaN;

            int n = sortedValues.Count;

            // 计算位置
            double position = (n + 1) * quartile / 4.0;
            int k = (int)position;
            double d = position - k;

            if (k <= 0) return sortedValues[0];
            if (k >= n) return sortedValues[n - 1];

            // 线性插值
            return sortedValues[k - 1] + d * (sortedValues[k] - sortedValues[k - 1]);
        }

        /// <summary>
        /// 计算百分位数
        /// </summary>
        public static double CalculatePercentile(List<double> sortedValues, double percentile)
        {
            if (sortedValues.Count == 0) return double.NaN;
            if (percentile < 0 || percentile > 100) return double.NaN;

            int n = sortedValues.Count;

            // 计算位置
            double position = (n + 1) * percentile / 100.0;
            int k = (int)position;
            double d = position - k;

            if (k <= 0) return sortedValues[0];
            if (k >= n) return sortedValues[n - 1];

            // 线性插值
            return sortedValues[k - 1] + d * (sortedValues[k] - sortedValues[k - 1]);
        }
    }

    #endregion Utility Classes

    #region Aggregate Strategies

    /// <summary>
    /// 基础聚合策略
    /// </summary>
    public abstract class AggregateStrategyBase : IAggregateStrategy
    {
        public abstract DgvAggregatorType AggregateType { get; }

        public abstract object Calculate(IEnumerable<object> values, Type dataType, Dictionary<string, object> parameters);

        public virtual Type GetResultColumnType(Type sourceType)
        {
            return sourceType ?? typeof(double);
        }

        /// <summary>
        /// 获取参数值
        /// </summary>
        protected T GetParameter<T>(Dictionary<string, object> parameters, string key, T defaultValue)
        {
            if (parameters != null && parameters.ContainsKey(key))
            {
                try
                {
                    return (T)Convert.ChangeType(parameters[key], typeof(T));
                }
                catch
                {
                    return defaultValue;
                }
            }
            return defaultValue;
        }
    }

    /// <summary>
    /// 求和策略
    /// </summary>
    public class SumAggregateStrategy : AggregateStrategyBase
    {
        public override DgvAggregatorType AggregateType => DgvAggregatorType.Sum;

        public override object Calculate(IEnumerable<object> values, Type dataType, Dictionary<string, object> parameters)
        {
            var validValues = MathHelper.FilterValidValues(values).ToList();
            if (validValues.Count == 0)
                return null;

            return validValues.Sum();
        }
    }

    /// <summary>
    /// 平均值策略
    /// </summary>
    public class AverageAggregateStrategy : AggregateStrategyBase
    {
        public override DgvAggregatorType AggregateType => DgvAggregatorType.Avg;

        public override object Calculate(IEnumerable<object> values, Type dataType, Dictionary<string, object> parameters)
        {
            var validValues = MathHelper.FilterValidValues(values).ToList();
            if (validValues.Count == 0)
                return null;

            return validValues.Average();
        }

        public override Type GetResultColumnType(Type sourceType)
        {
            if (sourceType == typeof(int) || sourceType == typeof(long))
                return typeof(double);
            return base.GetResultColumnType(sourceType);
        }
    }

    /// <summary>
    /// 最大值策略
    /// </summary>
    public class MaxAggregateStrategy : AggregateStrategyBase
    {
        public override DgvAggregatorType AggregateType => DgvAggregatorType.Max;

        public override object Calculate(IEnumerable<object> values, Type dataType, Dictionary<string, object> parameters)
        {
            var validValues = MathHelper.FilterValidValues(values).ToList();
            if (validValues.Count == 0)
                return null;

            return validValues.Max();
        }
    }

    /// <summary>
    /// 最小值策略
    /// </summary>
    public class MinAggregateStrategy : AggregateStrategyBase
    {
        public override DgvAggregatorType AggregateType => DgvAggregatorType.Min;

        public override object Calculate(IEnumerable<object> values, Type dataType, Dictionary<string, object> parameters)
        {
            var validValues = MathHelper.FilterValidValues(values).ToList();
            if (validValues.Count == 0)
                return null;

            return validValues.Min();
        }
    }

    /// <summary>
    /// 计数策略
    /// </summary>
    public class CountAggregateStrategy : AggregateStrategyBase
    {
        public override DgvAggregatorType AggregateType => DgvAggregatorType.Count;

        public override object Calculate(IEnumerable<object> values, Type dataType, Dictionary<string, object> parameters)
        {
            return values.Count();
        }

        public override Type GetResultColumnType(Type sourceType)
        {
            return typeof(int);
        }
    }

    /// <summary>
    /// 去重计数策略
    /// </summary>
    public class DistinctCountAggregateStrategy : AggregateStrategyBase
    {
        public override DgvAggregatorType AggregateType => DgvAggregatorType.DistinctCount;

        public override object Calculate(IEnumerable<object> values, Type dataType, Dictionary<string, object> parameters)
        {
            return values.Distinct().Count();
        }

        public override Type GetResultColumnType(Type sourceType)
        {
            return typeof(int);
        }
    }

    /// <summary>
    /// 方差策略（样本方差）
    /// </summary>
    public class VarianceAggregateStrategy : AggregateStrategyBase
    {
        public override DgvAggregatorType AggregateType => DgvAggregatorType.Variance;

        public override object Calculate(IEnumerable<object> values, Type dataType, Dictionary<string, object> parameters)
        {
            var validValues = MathHelper.FilterValidValues(values).ToList();
            if (validValues.Count < 2)
                return null;

            double mean = validValues.Average();
            double variance = validValues.Sum(x => Math.Pow(x - mean, 2)) / (validValues.Count - 1);
            return variance;
        }

        public override Type GetResultColumnType(Type sourceType)
        {
            return typeof(double);
        }
    }

    /// <summary>
    /// 标准差策略（样本标准差）
    /// </summary>
    public class StandardDeviationAggregateStrategy : AggregateStrategyBase
    {
        public override DgvAggregatorType AggregateType => DgvAggregatorType.StandardDeviation;

        public override object Calculate(IEnumerable<object> values, Type dataType, Dictionary<string, object> parameters)
        {
            var validValues = MathHelper.FilterValidValues(values).ToList();
            if (validValues.Count < 2)
                return null;

            double mean = validValues.Average();
            double variance = validValues.Sum(x => Math.Pow(x - mean, 2)) / (validValues.Count - 1);
            return Math.Sqrt(variance);
        }

        public override Type GetResultColumnType(Type sourceType)
        {
            return typeof(double);
        }
    }

    /// <summary>
    /// 中位数策略
    /// </summary>
    public class MedianAggregateStrategy : AggregateStrategyBase
    {
        public override DgvAggregatorType AggregateType => DgvAggregatorType.Median;

        public override object Calculate(IEnumerable<object> values, Type dataType, Dictionary<string, object> parameters)
        {
            var validValues = MathHelper.FilterValidValues(values).OrderBy(v => v).ToList();
            if (validValues.Count == 0)
                return null;

            int count = validValues.Count;
            if (count % 2 == 0)
            {
                return (validValues[count / 2 - 1] + validValues[count / 2]) / 2.0;
            }
            else
            {
                return validValues[count / 2];
            }
        }
    }

    /// <summary>
    /// 自定义聚合策略
    /// </summary>
    public class CustomAggregateStrategy : AggregateStrategyBase
    {
        public override DgvAggregatorType AggregateType => DgvAggregatorType.Custom;

        public override object Calculate(IEnumerable<object> values, Type dataType, Dictionary<string, object> parameters)
        {
            // 自定义聚合需要特殊的处理，这里默认返回总和
            var validValues = MathHelper.FilterValidValues(values).ToList();
            if (validValues.Count == 0)
                return null;

            return validValues.Sum();
        }
    }

    /// <summary>
    /// 乘积策略
    /// </summary>
    public class ProductAggregateStrategy : AggregateStrategyBase
    {
        public override DgvAggregatorType AggregateType => DgvAggregatorType.Product;

        public override object Calculate(IEnumerable<object> values, Type dataType, Dictionary<string, object> parameters)
        {
            var validValues = MathHelper.FilterValidValues(values).ToList();
            if (validValues.Count == 0)
                return null;

            double product = 1;
            foreach (var value in validValues)
            {
                product *= value;
            }

            return product;
        }
    }

    /// <summary>
    /// 乘积求和策略
    /// </summary>
    public class SumProductAggregateStrategy : AggregateStrategyBase
    {
        public override DgvAggregatorType AggregateType => DgvAggregatorType.SumProduct;

        public override object Calculate(IEnumerable<object> values, Type dataType, Dictionary<string, object> parameters)
        {
            // 对于SumProduct，values已经是每行的乘积结果
            var validValues = MathHelper.FilterValidValues(values).ToList();
            if (validValues.Count == 0)
                return null;

            return validValues.Sum();
        }
    }

    /// <summary>
    /// 众数策略
    /// </summary>
    public class ModeAggregateStrategy : AggregateStrategyBase
    {
        public override DgvAggregatorType AggregateType => DgvAggregatorType.Mode;

        public override object Calculate(IEnumerable<object> values, Type dataType, Dictionary<string, object> parameters)
        {
            var validValues = values.Where(v => v != null && v != DBNull.Value);
            if (!validValues.Any())
                return null;

            var groups = validValues.GroupBy(v => v)
                                   .OrderByDescending(g => g.Count());

            return groups.FirstOrDefault()?.Key;
        }
    }

    /// <summary>
    /// 范围策略（极差）
    /// </summary>
    public class RangeAggregateStrategy : AggregateStrategyBase
    {
        public override DgvAggregatorType AggregateType => DgvAggregatorType.Range;

        public override object Calculate(IEnumerable<object> values, Type dataType, Dictionary<string, object> parameters)
        {
            var validValues = MathHelper.FilterValidValues(values).ToList();
            if (validValues.Count < 2)
                return null;

            return validValues.Max() - validValues.Min();
        }

        public override Type GetResultColumnType(Type sourceType)
        {
            return typeof(double);
        }
    }

    /// <summary>
    /// 绝对值和策略
    /// </summary>
    public class SumAbsoluteAggregateStrategy : AggregateStrategyBase
    {
        public override DgvAggregatorType AggregateType => DgvAggregatorType.SumAbsolute;

        public override object Calculate(IEnumerable<object> values, Type dataType, Dictionary<string, object> parameters)
        {
            var validValues = MathHelper.FilterValidValues(values).ToList();
            if (validValues.Count == 0)
                return null;

            return validValues.Sum(Math.Abs);
        }
    }

    /// <summary>
    /// 平方和策略
    /// </summary>
    public class SumSquaresAggregateStrategy : AggregateStrategyBase
    {
        public override DgvAggregatorType AggregateType => DgvAggregatorType.SumSquares;

        public override object Calculate(IEnumerable<object> values, Type dataType, Dictionary<string, object> parameters)
        {
            var validValues = MathHelper.FilterValidValues(values).ToList();
            if (validValues.Count == 0)
                return null;

            return validValues.Sum(x => x * x);
        }
    }

    /// <summary>
    /// 均方根策略
    /// </summary>
    public class RootMeanSquareAggregateStrategy : AggregateStrategyBase
    {
        public override DgvAggregatorType AggregateType => DgvAggregatorType.RootMeanSquare;

        public override object Calculate(IEnumerable<object> values, Type dataType, Dictionary<string, object> parameters)
        {
            var validValues = MathHelper.FilterValidValues(values).ToList();
            if (validValues.Count == 0)
                return null;

            double sumSquares = validValues.Sum(x => x * x);
            return Math.Sqrt(sumSquares / validValues.Count);
        }

        public override Type GetResultColumnType(Type sourceType)
        {
            return typeof(double);
        }
    }

    /// <summary>
    /// 总体方差策略
    /// </summary>
    public class PopulationVarianceAggregateStrategy : AggregateStrategyBase
    {
        public override DgvAggregatorType AggregateType => DgvAggregatorType.PopulationVariance;

        public override object Calculate(IEnumerable<object> values, Type dataType, Dictionary<string, object> parameters)
        {
            var validValues = MathHelper.FilterValidValues(values).ToList();
            if (validValues.Count == 0)
                return null;

            double mean = validValues.Average();
            double variance = validValues.Sum(x => Math.Pow(x - mean, 2)) / validValues.Count;
            return variance;
        }

        public override Type GetResultColumnType(Type sourceType)
        {
            return typeof(double);
        }
    }

    /// <summary>
    /// 总体标准差策略
    /// </summary>
    public class PopulationStandardDeviationAggregateStrategy : AggregateStrategyBase
    {
        public override DgvAggregatorType AggregateType => DgvAggregatorType.PopulationStandardDeviation;

        public override object Calculate(IEnumerable<object> values, Type dataType, Dictionary<string, object> parameters)
        {
            var validValues = MathHelper.FilterValidValues(values).ToList();
            if (validValues.Count == 0)
                return null;

            double mean = validValues.Average();
            double variance = validValues.Sum(x => Math.Pow(x - mean, 2)) / validValues.Count;
            return Math.Sqrt(variance);
        }

        public override Type GetResultColumnType(Type sourceType)
        {
            return typeof(double);
        }
    }

    /// <summary>
    /// 四分位数策略
    /// </summary>
    public class QuartileAggregateStrategy : AggregateStrategyBase
    {
        public override DgvAggregatorType AggregateType => DgvAggregatorType.Quartile;

        public override object Calculate(IEnumerable<object> values, Type dataType, Dictionary<string, object> parameters)
        {
            var validValues = MathHelper.FilterValidValues(values).OrderBy(v => v).ToList();
            if (validValues.Count == 0)
                return null;

            int quartile = GetParameter(parameters, "quartile", 1);
            return MathHelper.CalculateQuartile(validValues, quartile);
        }

        public override Type GetResultColumnType(Type sourceType)
        {
            return typeof(double);
        }
    }

    /// <summary>
    /// 百分位数策略
    /// </summary>
    public class PercentileAggregateStrategy : AggregateStrategyBase
    {
        public override DgvAggregatorType AggregateType => DgvAggregatorType.Percentile;

        public override object Calculate(IEnumerable<object> values, Type dataType, Dictionary<string, object> parameters)
        {
            var validValues = MathHelper.FilterValidValues(values).OrderBy(v => v).ToList();
            if (validValues.Count == 0)
                return null;

            double percentile = GetParameter(parameters, "percentile", 50.0);
            return MathHelper.CalculatePercentile(validValues, percentile);
        }

        public override Type GetResultColumnType(Type sourceType)
        {
            return typeof(double);
        }
    }

    /// <summary>
    /// 偏度策略
    /// </summary>
    public class SkewnessAggregateStrategy : AggregateStrategyBase
    {
        public override DgvAggregatorType AggregateType => DgvAggregatorType.Skewness;

        public override object Calculate(IEnumerable<object> values, Type dataType, Dictionary<string, object> parameters)
        {
            var validValues = MathHelper.FilterValidValues(values).ToList();
            if (validValues.Count < 3)
                return null;

            return MathHelper.CalculateSkewness(validValues);
        }

        public override Type GetResultColumnType(Type sourceType)
        {
            return typeof(double);
        }
    }

    /// <summary>
    /// 峰度策略
    /// </summary>
    public class KurtosisAggregateStrategy : AggregateStrategyBase
    {
        public override DgvAggregatorType AggregateType => DgvAggregatorType.Kurtosis;

        public override object Calculate(IEnumerable<object> values, Type dataType, Dictionary<string, object> parameters)
        {
            var validValues = MathHelper.FilterValidValues(values).ToList();
            if (validValues.Count < 4)
                return null;

            return MathHelper.CalculateKurtosis(validValues);
        }

        public override Type GetResultColumnType(Type sourceType)
        {
            return typeof(double);
        }
    }

    /// <summary>
    /// 变异系数策略
    /// </summary>
    public class CoefficientOfVariationAggregateStrategy : AggregateStrategyBase
    {
        public override DgvAggregatorType AggregateType => DgvAggregatorType.CoefficientOfVariation;

        public override object Calculate(IEnumerable<object> values, Type dataType, Dictionary<string, object> parameters)
        {
            var validValues = MathHelper.FilterValidValues(values).ToList();
            if (validValues.Count < 2)
                return null;

            double mean = validValues.Average();
            if (mean == 0) return double.NaN;

            double variance = validValues.Sum(x => Math.Pow(x - mean, 2)) / (validValues.Count - 1);
            double stdDev = Math.Sqrt(variance);

            return stdDev / Math.Abs(mean);
        }

        public override Type GetResultColumnType(Type sourceType)
        {
            return typeof(double);
        }
    }

    /// <summary>
    /// 几何平均数策略
    /// </summary>
    public class GeometricMeanAggregateStrategy : AggregateStrategyBase
    {
        public override DgvAggregatorType AggregateType => DgvAggregatorType.GeometricMean;

        public override object Calculate(IEnumerable<object> values, Type dataType, Dictionary<string, object> parameters)
        {
            var validValues = MathHelper.FilterValidValues(values).ToList();
            if (validValues.Count == 0)
                return null;

            return MathHelper.CalculateGeometricMean(validValues);
        }

        public override Type GetResultColumnType(Type sourceType)
        {
            return typeof(double);
        }
    }

    /// <summary>
    /// 调和平均数策略
    /// </summary>
    public class HarmonicMeanAggregateStrategy : AggregateStrategyBase
    {
        public override DgvAggregatorType AggregateType => DgvAggregatorType.HarmonicMean;

        public override object Calculate(IEnumerable<object> values, Type dataType, Dictionary<string, object> parameters)
        {
            var validValues = MathHelper.FilterValidValues(values).ToList();
            if (validValues.Count == 0)
                return null;

            return MathHelper.CalculateHarmonicMean(validValues);
        }

        public override Type GetResultColumnType(Type sourceType)
        {
            return typeof(double);
        }
    }

    #endregion Aggregate Strategies

    #region Core Aggregator

    /// <summary>
    /// 增强的数据聚合器
    /// </summary>
    public class DgvAggregator
    {
        private readonly IAggregateStrategyFactory _strategyFactory;
        private readonly IExpressionCalculator _expressionCalculator;

        public DgvAggregator()
            : this(new AggregateStrategyFactory(), new ExpressionCalculator())
        {
        }

        public DgvAggregator(IAggregateStrategyFactory strategyFactory, IExpressionCalculator expressionCalculator)
        {
            _strategyFactory = strategyFactory ?? throw new ArgumentNullException(nameof(strategyFactory));
            _expressionCalculator = expressionCalculator ?? throw new ArgumentNullException(nameof(expressionCalculator));
        }

        #region 静态方法（保持向后兼容）

        /// <summary>
        /// 执行分组聚合（兼容旧版本）
        /// </summary>
        public static DataTable AggregateDataTable(
            DataTable sourceTable,
            List<string> groupColumns,
            List<DgvAggregatorColumnConfig> aggregateConfigs)
        {
            var config = new DgvAggregatorConfig
            {
                GroupColumns = groupColumns,
                AggregateColumns = aggregateConfigs
            };

            return new DgvAggregator().Aggregate(sourceTable, config);
        }

        /// <summary>
        /// 执行分组聚合（新版本）
        /// </summary>
        public static DataTable AggregateDataTable(
            DataTable sourceTable,
            DgvAggregatorConfig config)
        {
            return new DgvAggregator().Aggregate(sourceTable, config);
        }

        #endregion 静态方法（保持向后兼容）

        /// <summary>
        /// 执行聚合
        /// </summary>
        public DataTable Aggregate(DataTable sourceTable, DgvAggregatorConfig config)
        {
            if (sourceTable == null)
                throw new ArgumentNullException(nameof(sourceTable));
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            if (sourceTable.Rows.Count == 0)
                return CreateResultTable(config.GroupColumns, config.AggregateColumns);

            // 分组数据
            var groups = GroupData(sourceTable, config.GroupColumns);

            // 创建结果表
            DataTable resultTable = CreateResultTable(config.GroupColumns, config.AggregateColumns);

            // 处理每个分组
            foreach (var group in groups)
            {
                DataRow newRow = resultTable.NewRow();

                // 填充分组列值
                FillGroupColumns(newRow, config.GroupColumns, group.Key);

                // 计算聚合值
                CalculateAggregates(newRow, config.AggregateColumns, group.Value);

                resultTable.Rows.Add(newRow);
            }

            return resultTable;
        }

        /// <summary>
        /// 分组数据
        /// </summary>
        private Dictionary<GroupKey, List<DataRow>> GroupData(DataTable sourceTable, List<string> groupColumns)
        {
            var groups = new Dictionary<GroupKey, List<DataRow>>();

            foreach (DataRow row in sourceTable.Rows)
            {
                var keyValues = groupColumns.Select(col =>
                {
                    var value = row[col];
                    return value is DBNull ? null : value;
                }).ToList();

                var groupKey = new GroupKey(keyValues);

                if (!groups.ContainsKey(groupKey))
                {
                    groups[groupKey] = new List<DataRow>();
                }
                groups[groupKey].Add(row);
            }

            return groups;
        }

        /// <summary>
        /// 创建结果数据表结构
        /// </summary>
        private DataTable CreateResultTable(
            List<string> groupColumns,
            List<DgvAggregatorColumnConfig> aggregateConfigs)
        {
            DataTable resultTable = new DataTable();

            // 添加分组列
            foreach (var col in groupColumns)
            {
                // 默认使用字符串类型作为分组列类型
                resultTable.Columns.Add(col, typeof(string));
            }

            // 添加聚合列
            foreach (var config in aggregateConfigs)
            {
                Type columnType = GetResultColumnType(config.AggregateType, config.DataType);
                resultTable.Columns.Add(config.ResultColumn, columnType);
            }

            return resultTable;
        }

        /// <summary>
        /// 填充分组列
        /// </summary>
        private void FillGroupColumns(DataRow newRow, List<string> groupColumns, GroupKey groupKey)
        {
            for (int i = 0; i < groupColumns.Count; i++)
            {
                newRow[groupColumns[i]] = groupKey.Values[i] ?? DBNull.Value;
            }
        }

        /// <summary>
        /// 计算聚合值
        /// </summary>
        private void CalculateAggregates(DataRow newRow,
            List<DgvAggregatorColumnConfig> aggregateConfigs,
            List<DataRow> rows)
        {
            foreach (var config in aggregateConfigs)
            {
                try
                {
                    object aggregatedValue = CalculateAggregateForGroup(rows, config);
                    newRow[config.ResultColumn] = aggregatedValue ?? DBNull.Value;
                }
                catch (Exception ex)
                {
                    // 记录错误，继续处理其他列
                    System.Diagnostics.Debug.WriteLine($"聚合计算失败: {ex.Message}");
                    newRow[config.ResultColumn] = DBNull.Value;
                }
            }
        }

        /// <summary>
        /// 计算单个分组的聚合值
        /// </summary>
        private object CalculateAggregateForGroup(
            List<DataRow> rows,
            DgvAggregatorColumnConfig config)
        {
            // 获取用于聚合的值列表
            List<object> values = GetAggregationValues(rows, config);

            // 使用策略计算聚合值
            var strategy = _strategyFactory.CreateStrategy(config.AggregateType);
            return strategy.Calculate(values, config.DataType, config.Parameters);
        }

        /// <summary>
        /// 获取用于聚合的值列表
        /// </summary>
        private List<object> GetAggregationValues(List<DataRow> rows, DgvAggregatorColumnConfig config)
        {
            var values = new List<object>();

            if (config.AggregateType == DgvAggregatorType.SumProduct)
            {
                // SumProduct需要计算每行的乘积
                foreach (DataRow row in rows)
                {
                    var product = CalculateRowProduct(config.SourceColumn, row);
                    if (product != null && product != DBNull.Value)
                    {
                        values.Add(product);
                    }
                }
            }
            else if (config.AggregateType == DgvAggregatorType.Custom)
            {
                // 自定义表达式
                foreach (DataRow row in rows)
                {
                    object value = _expressionCalculator.EvaluateExpression(config.CustomExpression, row);
                    if (value != null && value != DBNull.Value)
                    {
                        values.Add(value);
                    }
                }
            }
            else if (config.AggregateType == DgvAggregatorType.Product)
            {
                // 乘积：每行获取单个值
                foreach (DataRow row in rows)
                {
                    object value = row[config.SourceColumn];
                    if (value != null && value != DBNull.Value)
                    {
                        values.Add(value);
                    }
                }
            }
            else
            {
                // 普通聚合：每行获取单个值
                foreach (DataRow row in rows)
                {
                    object value = row[config.SourceColumn];
                    if (value != null && value != DBNull.Value)
                    {
                        values.Add(value);
                    }
                }
            }

            return values;
        }

        /// <summary>
        /// 计算行的乘积
        /// </summary>
        private object CalculateRowProduct(string expression, DataRow row)
        {
            try
            {
                List<object> values = _expressionCalculator.ParseProductExpression(expression, row);
                if (values.Count == 0)
                    return null;

                double product = 1;
                foreach (object val in values)
                {
                    double d = MathHelper.ToDouble(val);
                    if (double.IsNaN(d))
                        return null;
                    product *= d;
                }
                return product;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 获取结果列的数据类型
        /// </summary>
        private Type GetResultColumnType(DgvAggregatorType aggregateType, Type sourceType)
        {
            var strategy = _strategyFactory.CreateStrategy(aggregateType);
            return strategy.GetResultColumnType(sourceType);
        }

        #region 分组键类

        /// <summary>
        /// 分组键类（支持多列分组）
        /// </summary>
        private class GroupKey
        {
            public List<object> Values { get; }

            public GroupKey(List<object> values)
            {
                Values = values;
            }

            public override bool Equals(object obj)
            {
                if (obj is GroupKey other)
                {
                    if (Values.Count != other.Values.Count)
                        return false;

                    for (int i = 0; i < Values.Count; i++)
                    {
                        if (!object.Equals(Values[i], other.Values[i]))
                            return false;
                    }
                    return true;
                }
                return false;
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    int hash = 17;
                    foreach (var value in Values)
                    {
                        hash = hash * 23 + (value?.GetHashCode() ?? 0);
                    }
                    return hash;
                }
            }
        }

        #endregion 分组键类
    }

    #endregion Core Aggregator

    #region Extensions

    /// <summary>
    /// 聚合器扩展方法
    /// </summary>
    public static class DgvAggregatorExtensions
    {
        /// <summary>
        /// 验证聚合配置是否有效
        /// </summary>
        public static bool ValidateConfig(this DgvAggregatorConfig config, DataTable sourceTable, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (config == null)
            {
                errorMessage = "配置不能为空";
                return false;
            }

            // 验证分组列
            foreach (string groupCol in config.GroupColumns)
            {
                if (!sourceTable.Columns.Contains(groupCol))
                {
                    errorMessage = $"分组列 '{groupCol}' 不存在于数据表中";
                    return false;
                }
            }

            // 验证聚合列
            foreach (var aggCol in config.AggregateColumns)
            {
                if (aggCol.AggregateType == DgvAggregatorType.Custom)
                {
                    if (string.IsNullOrEmpty(aggCol.CustomExpression))
                    {
                        errorMessage = "自定义聚合类型必须指定CustomExpression";
                        return false;
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(aggCol.SourceColumn))
                    {
                        errorMessage = "聚合列必须指定SourceColumn";
                        return false;
                    }

                    // 检查源列是否存在（对于乘积表达式，可能有多个列）
                    if (!aggCol.SourceColumn.Contains("*"))
                    {
                        if (!sourceTable.Columns.Contains(aggCol.SourceColumn))
                        {
                            errorMessage = $"源列 '{aggCol.SourceColumn}' 不存在于数据表中";
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// 注册自定义聚合策略
        /// </summary>
        public static void RegisterCustomStrategy(this DgvAggregator aggregator,
            DgvAggregatorType aggregateType, IAggregateStrategy strategy)
        {
            if (aggregator == null)
                throw new ArgumentNullException(nameof(aggregator));

            var strategyFactory = new AggregateStrategyFactory();
            strategyFactory.RegisterStrategy(aggregateType, strategy);
        }
    }

    #endregion Extensions
}