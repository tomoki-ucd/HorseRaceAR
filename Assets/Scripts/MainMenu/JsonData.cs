using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public sealed class JsonData 
{
    public List<Message> messages;
    public string model;
    public bool stream;
    public int temperature;
}


public sealed class Message
{
    public string role;
    public string content;
}