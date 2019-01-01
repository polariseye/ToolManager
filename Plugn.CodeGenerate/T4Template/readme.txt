基于TableHost生成代码。每个表都会执行一次这个模板
TableHost的核心成员：
		/// <summary>
        /// 表信息
        /// </summary>
        public SOTable Table { get; set; }

        /// <summary>
        /// 列信息
        /// </summary>
        public List<SOColumn> ColumnList