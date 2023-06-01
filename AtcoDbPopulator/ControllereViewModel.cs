// <copyright file="ControllereViewModel.cs" company="Leonardo Tassinari">
// Copyright (c) Leonardo Tassinari. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace AtcoDbPopulator;

/// <summary>
/// Short Controller view.
/// </summary>
public class ControlloreViewModel
{
    /// <summary>
    /// Gets or sets id of the controller.
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// Gets or sets name of the controller.
    /// </summary>
    public string? Nome { get; set; }

    /// <summary>
    /// Gets or sets the Surname of the controller.
    /// </summary>
    public string? Cognome { get; set; }

    /// <summary>
    /// Gets or sets the total worked shifts.
    /// </summary>
    public int? TurniLavorati { get; set; }
}