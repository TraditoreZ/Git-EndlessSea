﻿using UnityEngine;
using System;
using Mono.Data.Sqlite;


public class DbAccess
{
	private SqliteConnection dbConnection;
	
	private SqliteCommand dbCommand;
	
	private SqliteDataReader reader;
	
	
	public DbAccess (string connectionString)
		
	{
		
		OpenDB (connectionString);
		
	}
	public DbAccess ()
	{
		
	}
	
	public void OpenDB (string connectionString)
		
	{
		try
		{
			dbConnection = new SqliteConnection (connectionString);
			
			dbConnection.Open ();
			
			Debug.Log ("Connected to db,连接数据库成功！");
		}
		catch(Exception e)
		{
			string temp1 = e.ToString();
			Debug.Log(temp1);
		}
		
	}
	
	
	
	public void CloseSqlConnection ()
		
	{
		
		if (dbCommand != null) {
			
			dbCommand.Dispose ();
			
		}
		
		dbCommand = null;
		
		if (reader != null) {
			
			reader.Dispose ();
			
		}
		
		reader = null;
		
		if (dbConnection != null) {
			
			dbConnection.Close ();
			
		}
		
		dbConnection = null;
		
		Debug.Log ("Disconnected from db.关闭数据库！");
		
	}
	
	
	public SqliteDataReader ExecuteQuery (string sqlQuery)
		
	{
		
		dbCommand = dbConnection.CreateCommand ();
		
		dbCommand.CommandText = sqlQuery;
	
		reader = dbCommand.ExecuteReader ();

		return reader;
		
	}
	
	
	public SqliteDataReader ReadFullTable (string tableName)
		
	{
		
		string query = "SELECT * FROM " + tableName;
		
		return ExecuteQuery (query);
		
	}
	
	
	/// <summary>
	/// 插入数据 param tableName=表名 values=数据内容
	/// </summary>
	public SqliteDataReader InsertInto (string tableName, string[] values)
		
	{
		
		string query = "INSERT INTO " + tableName + " VALUES (" + values[0];
		
		for (int i = 1; i < values.Length; ++i) {
			
			query += ", " + values[i];
			
		}
		
		query += ")";
		
		return ExecuteQuery (query);
		
	}
	
	/// <summary>
	/// 插入数据 param tableName=表名 cols=更新字段 colsvalues=更新内容 selectkey=查找字段（主键) selectvalue=查找内容
	/// </summary>
	public SqliteDataReader UpdateInto (string tableName, string []cols,string []colsvalues,string selectkey,string selectvalue)
	{
		
		
		string query = "UPDATE "+tableName+" SET "+cols[0]+" = "+colsvalues[0];
		
		for (int i = 1; i < colsvalues.Length; ++i) {
			
			query += ", " +cols[i]+" ="+ colsvalues[i];
		}
		
		query += " WHERE "+selectkey+" = "+selectvalue+" ";
		
		return ExecuteQuery (query);
	}
	
	/// <summary>
	/// 删除数据 param tableName=表名 cols=字段 colsvalues=内容
	/// </summary>
	public SqliteDataReader Delete(string tableName,string []cols,string []colsvalues)
	{
		string query = "DELETE FROM "+tableName + " WHERE " +cols[0] +" = " + colsvalues[0];
		
		for (int i = 1; i < colsvalues.Length; ++i) {
			
			query += " or " +cols[i]+" = "+ colsvalues[i];
		}
		return ExecuteQuery (query);
	}
	
	
	public SqliteDataReader InsertIntoSpecific (string tableName, string[] cols, string[] values)
		
	{
		
		if (cols.Length != values.Length) {
			
			throw new SqliteException ("columns.Length != values.Length");
			
		}
		
		string query = "INSERT INTO " + tableName + "(" + cols[0];
		
		for (int i = 1; i < cols.Length; ++i) {
			
			query += ", " + cols[i];
			
		}
		
		query += ") VALUES (" + values[0];
		
		for (int i = 1; i < values.Length; ++i) {
			
			query += ", " + values[i];
			
		}
		
		query += ")";
		
		return ExecuteQuery (query);
		
	}
	
	
	
	public SqliteDataReader DeleteContents (string tableName)
		
	{
		
		string query = "DELETE FROM " + tableName;
		
		return ExecuteQuery (query);
		
	}
	
	/// <summary>
	/// 创建表 param name=表名 col=字段名 colType=字段类型
	/// </summary>
	public SqliteDataReader CreateTable (string name, string[] col, string[] colType)
		
	{
		
		if (col.Length != colType.Length) {
			
			throw new SqliteException ("columns.Length != colType.Length");
			
		}
		
		string query = "CREATE TABLE " + name + " (" + col[0] + " " + colType[0];
		
		for (int i = 1; i < col.Length; ++i) {
			
			query += ", " + col[i] + " " + colType[i];
			
		}
		
		query += ")";
		
		
		return ExecuteQuery (query);
		
	}
	
	
	
	
	
	/// <summary>
	/// 插入数据 param tableName=表名 items=结果字段 col=查找字段 operation=运算符 values=内容
	/// </summary>
	public SqliteDataReader SelectWhere (string tableName, string[] items, string[] col, string[] operation, string[] values)	
	{	
		if (col.Length != operation.Length || operation.Length != values.Length) {	
			throw new SqliteException ("col.Length != operation.Length != values.Length");	
		}
		string query = "SELECT " + items[0];
		for (int i = 1; i < items.Length; ++i) {	
			query += ", " + items[i];
		}
		query += " FROM " + tableName + " WHERE " + col[0] + operation[0] + "'" + values[0] + "' ";
		for (int i = 1; i < col.Length; ++i) {
			query += " AND " + col[i] + operation[i] + "'" + values[i] + "' ";
		}
		return ExecuteQuery (query);
	}

	/// <summary>
	/// 查询表
	/// </summary>
	public SqliteDataReader Select(string tableName, string col, string values)
	{
		string query = "SELECT * FROM " + tableName  + " WHERE " + col + " = " + values;
		return ExecuteQuery (query);
	}

	public SqliteDataReader Select(string tableName, string col,string operation, string values)
	{
		string query = "SELECT * FROM " + tableName  + " WHERE " + col + operation + values;
		return ExecuteQuery (query);
	}

	
}