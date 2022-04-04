// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using CommunityToolkit.Mvvm.Collections.Internals;

namespace CommunityToolkit.Mvvm.Collections;

/// <summary>
/// An observable group.
/// It associates a <see cref="Key"/> to an <see cref="ObservableCollection{T}"/>.
/// </summary>
/// <typeparam name="TKey">The type of the group key.</typeparam>
/// <typeparam name="TElement">The type of elements in the group.</typeparam>
[DebuggerDisplay("Key = {Key}, Count = {Count}")]
public sealed class ObservableGroup<TKey, TElement> : ObservableCollection<TElement>, IReadOnlyObservableGroup<TKey, TElement>
    where TKey : notnull
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ObservableGroup{TKey, TValue}"/> class.
    /// </summary>
    /// <param name="key">The key for the group.</param>
    public ObservableGroup(TKey key)
    {
        this.key = key;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ObservableGroup{TKey, TValue}"/> class.
    /// </summary>
    /// <param name="grouping">The grouping to fill the group.</param>
    public ObservableGroup(IGrouping<TKey, TElement> grouping)
        : base(grouping)
    {
        this.key = grouping.Key;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ObservableGroup{TKey, TValue}"/> class.
    /// </summary>
    /// <param name="key">The key for the group.</param>
    /// <param name="collection">The initial collection of data to add to the group.</param>
    public ObservableGroup(TKey key, IEnumerable<TElement> collection)
        : base(collection)
    {
        this.key = key;
    }

    private TKey key;

    /// <summary>
    /// Gets or sets the key of the group.
    /// </summary>
    public TKey Key
    {
        get => this.key;
        set
        {
            if (!EqualityComparer<TKey>.Default.Equals(this.key!, value))
            {
                this.key = value;

                OnPropertyChanged(ObservableGroupHelper.KeyChangedEventArgs);
            }
        }
    }

    /// <inheritdoc/>
    object IReadOnlyObservableGroup.Key => Key;

    /// <inheritdoc/>
    object? IReadOnlyObservableGroup.this[int index] => this[index];
}
