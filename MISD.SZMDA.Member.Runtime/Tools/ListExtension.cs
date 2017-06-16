using System;
using System.Collections.Generic;

namespace MISD.SZMDA.Member.Runtime.Tools
{

    public static class ListExtension
    {
        /// <summary>
        /// 拓扑排序（用于排序带有依赖的序列）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"> 需要排序的序列，<see cref="IEnumerable&lt;T&gt;" />类型的数组或者集合</param>
        /// <param name="getDependencies">序列项包含的依赖项，<see cref="IEnumerable&lt;T&gt;" />类型的数组或者集合</param>
        /// <returns>排序完成后的序列仓储，<see cref="IList&lt;T&gt;" />类型</returns>
        public static List<T> SortByDependencies<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> getDependencies)
        {
            //排序完成后的序列仓储
            var sorted = new List<T>();

            //用于标识
            var visited = new Dictionary<T, bool>();

            foreach (var item in source)
            {
                SortByDependenciesVisit(item, getDependencies, sorted, visited);
            }

            return sorted;
        }


        /// <summary>
        /// 访问序列内容进行排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item">单个序列</param>
        /// <param name="getDependencies">该序列的依赖项</param>
        /// <param name="sorted">排序完成的集合</param>
        /// <param name="visited">跟踪标识集合</param>
        public static void SortByDependenciesVisit<T>(T item, Func<T, IEnumerable<T>> getDependencies, List<T> sorted, Dictionary<T, bool> visited)
        {
            bool inProcess;
            //判断序列是否为空
            var alreadyVisited = visited.TryGetValue(item, out inProcess);

            if (alreadyVisited)
            {
                if (inProcess)
                {
                    throw new ArgumentException("Cyclic dependency found");
                }
            }
            else
            {
                //设置标识为正在跟踪的状态
                visited[item] = true;
                var dependencies = getDependencies(item);

                if (dependencies != null)
                {
                    //获取该序列的依赖并开始递归获取依赖关系
                    foreach (var dependency in dependencies)
                    {
                        SortByDependenciesVisit(dependency, getDependencies, sorted, visited);
                    }
                }
                //访问之后将状态重新设置为未跟踪
                visited[item] = false;
                //添加序列到sorted集合，说明排序成功
                sorted.Add(item);
            }
        }
    }
}

