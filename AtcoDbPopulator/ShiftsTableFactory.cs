// <copyright file="ShiftsTableFactory.cs" company="Leonardo Tassinari">
// Copyright (c) Leonardo Tassinari. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace AtcoDbPopulator;

/// <summary>
/// Factory to create a table for the shift of a specific month.
/// </summary>
public class ShiftsTableFactory
{
    private readonly Dictionary<string, AtcoDbPopulator.Models.Controllore> controllers = new ();
    private readonly Dictionary<string, ISet<AtcoDbPopulator.Models.Turno>> standByPositions = new ();

    /// <summary>
    /// Initializes a new instance of the <see cref="ShiftsTableFactory"/> class.
    /// </summary>
    public ShiftsTableFactory()
    {
        using var dbContext = new AtcoDbPopulator.Models.AtctablesContext();
        var standByShifts = dbContext.Turnos.Where(s => s.CentroStandBy != null).ToHashSet();
        foreach (var shift in standByShifts)
        {
            if (!this.standByPositions.ContainsKey(shift.CentroStandBy!))
            {
                this.standByPositions.Add(shift.CentroStandBy!, new HashSet<AtcoDbPopulator.Models.Turno>());
            }

            this.standByPositions[shift.CentroStandBy!].Add(shift);
        }
    }

    /// <summary>
    /// Method to create a table for the shift of a specific month.
    /// </summary>
    /// <param name="month">The relative month.</param>
    /// <param name="year">The relative year.</param>
    /// <returns>The specific table.</returns>
    public System.Data.DataTable CreateDataTable(int month, int year)
    {
        using var dbContext = new AtcoDbPopulator.Models.AtctablesContext();
        var positions = dbContext.Postaziones.Select(p => p.IdPostazione);

        List<string> dates = new List<string>();
        DateTime startDate = new DateTime(year, month, 1);
        var shifts = dbContext.Turnos
            .Where(t =>
                t.Data.Year == year && t.Data.Month == month)
            .ToHashSet();

        while (startDate.Month == month)
        {
            for (int i = 1; i <= CenterTurns.ShiftsInDays; i++)
            {
                dates.Add(startDate.ToShortDateString() + ", turno " + i);
            }

            startDate = startDate.AddDays(1);
        }

        var dataTable = new System.Data.DataTable();
        dataTable.Columns.Add("Position", typeof(string));
        foreach (var date in dates)
        {
            dataTable.Columns.Add(date);
        }

        var headerRow = dataTable.NewRow();
        headerRow["Position"] = "Postazioni:";
        foreach (var date in dates)
        {
            headerRow[date] = date;
        }

        dataTable.Rows.Add(headerRow);

        foreach (var position in positions)
        {
            var row = dataTable.NewRow();
            row["Position"] = position;

            // Fill the row with data for each date
            foreach (var date in dates)
            {
                var turno = shifts.FirstOrDefault(t =>
                    t.IdPostazione == position &&
                    date == t.Data.ToShortDateString() + ", turno " + t.Slot);
                if (turno != null)
                {
                    AtcoDbPopulator.Models.Controllore? controller = this.FindController(turno.IdControllore);
                    row[date] = controller != null
                        ? turno.IdControllore + " " + controller.Nome + " " + controller.Cognome
                        : string.Empty;
                }
                else
                {
                    row[date] = string.Empty;
                }
            }

            dataTable.Rows.Add(row);
        }

        // Fill standby rows
        foreach (var position in this.standByPositions.Keys)
        {
            var row = dataTable.NewRow();
            row["Position"] = position + " STBY";

            // Fill the row with data for each date
            foreach (var date in dates)
            {
                var stbyShifts = this.standByPositions[position].Where(s => date.Equals(s.Data.ToShortDateString() + ", turno " + s.Slot)).ToHashSet();
                if (stbyShifts.Any())
                {
                    string resString = string.Empty;
                    foreach (var shift in stbyShifts)
                    {
                        AtcoDbPopulator.Models.Controllore? controller = this.FindController(shift.IdControllore);
                        resString += controller != null ? shift.IdControllore + " " + controller.Nome + " " + controller.Cognome + "\n"
                            : string.Empty;
                    }

                    row[date] = resString;
                }
                else
                {
                    row[date] = string.Empty;
                }
            }

            dataTable.Rows.Add(row);
        }

        return dataTable;
    }

    private AtcoDbPopulator.Models.Controllore? FindController(string idController)
    {
        if (this.controllers.TryGetValue(idController, out AtcoDbPopulator.Models.Controllore? dictResult))
        {
            return dictResult;
        }

        using var dbContext = new AtcoDbPopulator.Models.AtctablesContext();

        dictResult = dbContext.Controllores.Find(idController);
        if (dictResult != null)
        {
            this.controllers.Add(idController, dictResult);
        }

        return dictResult;
    }
}