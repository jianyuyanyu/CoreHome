﻿using DataContext.DbConfig;
using DataContext.ModelDbContext;
using DataContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataContext.DbOperator
{
    public class CommentOperator : IDbOperator<Comment>
    {
        private readonly DbConfigurator configurator;

        public CommentOperator()
        {
            configurator = new DbConfigurator();
        }

        /// <summary>
        /// 新增评论
        /// </summary>
        /// <param name="t"></param>
        public void Add(Comment t)
        {
            using ArticleDbContext context = configurator.CreateArticleDbContext();
            context.Comment.Add(t);
            context.SaveChanges();
        }

        /// <summary>
        /// 统计评论总数
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            using ArticleDbContext context = configurator.CreateArticleDbContext();
            return context.Comment.Count();
        }

        /// <summary>
        /// 删除评论
        /// </summary>
        /// <param name="commentID">评论ID</param>
        public void Delete(string commentID)
        {
            using ArticleDbContext context = configurator.CreateArticleDbContext();
            context.Comment.Remove(Find(commentID));
            context.SaveChanges();
        }

        /// <summary>
        /// 查找评论
        /// </summary>
        /// <param name="commentID">评论ID</param>
        /// <returns>评论实体</returns>
        public Comment Find(string commentID)
        {
            using ArticleDbContext context = configurator.CreateArticleDbContext();
            Comment comment = context.Comment.Single(i => i.CommentID == commentID);
            return comment;
        }

        /// <summary>
        /// 范围查找评论
        /// </summary>
        /// <param name="func">查询条件</param>
        /// <param name="start">起始索引</param>
        /// <param name="count">查询数量</param>
        /// <returns></returns>
        public List<Comment> Find(Func<Comment, bool> func, int start, int count)
        {
            using ArticleDbContext context = configurator.CreateArticleDbContext();
            List<Comment> comments = context.Comment.Where(func).OrderByDescending(i => i.ID).ToList();
            return comments;
        }

        public void Modify(Comment newModel)
        {
            throw new NotImplementedException();
        }
    }
}
