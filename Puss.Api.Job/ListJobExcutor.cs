﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Puss.Api.Job
{
    /// <summary>
    /// ListJobExcutor
    /// </summary>
    /// <typeparam name="IType"></typeparam>
    public abstract class ListJobExcutor<IType>
: IJobExecutor
    {
        /// <summary>
        ///  运行状态
        /// </summary>
        public bool IsRuning { get; protected set; }

        /// <summary>
        ///   开始任务
        /// </summary>
        public void StartJob()
        {
            //任务依然在执行中，不需要再次唤起
            if (IsRuning)
                return;

            IsRuning = true;
            IList<IType> list = null; // 结清实体list
            do
            {
                for (var i = 0; IsRuning && i < list?.Count; i++)
                {
                    ExcuteItem(list[i], i);
                }
                list = GetExcuteSource();

            } while (IsRuning && list?.Count > 0);
            IsRuning = false;
        }

        /// <summary>
        /// 结束任务
        /// </summary>
        public void StopJob()
        {
            IsRuning = false;
        }

        /// <summary>
        ///   获取list数据源
        /// </summary>
        /// <returns></returns>
        protected virtual IList<IType> GetExcuteSource()
        {
            return null;
        }

        /// <summary>
        ///  个体任务执行
        /// </summary>
        /// <param name="item">单个实体</param>
        /// <param name="index">在数据源中的索引</param>
        protected virtual void ExcuteItem(IType item, int index)
        {
        }
    }
}
