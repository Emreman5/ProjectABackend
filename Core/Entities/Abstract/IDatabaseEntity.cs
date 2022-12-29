using System;

namespace Core.Entities.Abstract;

public interface IDatabaseEntity
{
    
}
public interface IDatabaseEntity<out TKey> : IDatabaseEntity where TKey : IEquatable<TKey>
{
    public TKey Id { get; }
    DateTime CreatedAt { get; set; }
    DateTime LastUpdate { get; set; }
}