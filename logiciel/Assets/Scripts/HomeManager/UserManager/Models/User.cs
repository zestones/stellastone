using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using System;
using UnityEngine;

public static class User
{
	private static string id;
	private static string username;
	private static string email;
	private static string description;
	
	private static string avatarUrl;
	private static Sprite avatar; 

	public static string Id
	{
		get { return id; }
		set { id = value; }
	}

	public static string Username
	{
		get { return username; }
		set { username = value; }
	}

	public static string Email
	{
		get { return email; }
		set { email = value; }
	}

	public static string Description
	{
		get { return description; }
		set { description = value; }
	}

	public static string AvatarUrl 
	{ 
		get { return avatarUrl; }
		set { avatarUrl = value; }
	}
	public static Sprite Avatar
	{
		get { return avatar; }
		set { avatar = value; }
	}
	
	public static void ForgetData() 
	{
		Id = null;
		Username = null;
		Email = null;
		Description = null;
		AvatarUrl = null;
		Avatar = null;
	}
}