using HouseRenting.Common.Dto.Services;
using HouseRenting.Core.Services.Contracts;
using HouseRenting.Dal.Entities;
using HouseRenting.Dal.Repositories.Contracts;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace HouseRenting.Core.Services;
public class ReportGenerator : IReportGenerator
{
    private readonly IQueryRepository<Dal.Entities.Order> _orderRepository;
    private readonly IQueryRepository<Advert> _advertRepository;

    public ReportGenerator(IQueryRepository<Dal.Entities.Order> orderRepository, IQueryRepository<Advert> advertRepository)
    {
        _orderRepository = orderRepository;
        _advertRepository = advertRepository;
    }

    public async Task<byte[]> CreateAvitoFileAsync(CancellationToken cancellationToken = default)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        var reportModels = await _advertRepository.GetAllAsync(cancellationToken);

        using (var package = new ExcelPackage())
        {
            var sheet = package.Workbook.Worksheets.Add("kvartiry_sdam");

            var idCell = sheet.Cells["A1"];
            SetCellProperties(idCell, "Id");

            var descriptionCell = sheet.Cells["B1"];
            SetCellProperties(descriptionCell, "Description");

            var addressCell = sheet.Cells["C1"];
            SetCellProperties(addressCell, "Address");

            var categoryCell = sheet.Cells["D1"];
            SetCellProperties(categoryCell, "Category");

            var priceCell = sheet.Cells["E1"];
            SetCellProperties(priceCell, "Price");

            var operationTypeCell = sheet.Cells["F1"];
            SetCellProperties(operationTypeCell, "OperationType");

            var floorCell = sheet.Cells["G1"];
            SetCellProperties(floorCell, "Floor");

            var roomsCell = sheet.Cells["H1"];
            SetCellProperties(roomsCell, "Rooms");

            var squareCell = sheet.Cells["I1"];
            SetCellProperties(squareCell, "Square");


            int row = 2;

            foreach (var advert in reportModels)
            {
                var idCellValue = sheet.Cells[row, 1];
                SetCellProperties(idCellValue, advert.Id.ToString());

                var descriptionCellValue = sheet.Cells[row, 2];
                SetCellProperties(descriptionCellValue, advert.Comment);

                var addressCellValue = sheet.Cells[row, 3];
                SetCellProperties(addressCellValue, advert.Address);

                var categoryCellValue = sheet.Cells[row, 4];
                SetCellProperties(categoryCellValue, "Квартиры");

                var priceCellValue = sheet.Cells[row, 5];
                SetCellProperties(priceCellValue, advert.Price.ToString());

                var operationTypeCellValue = sheet.Cells[row, 6];
                SetCellProperties(operationTypeCellValue, "Сдам");

                var floorCellValue = sheet.Cells[row, 7];
                SetCellProperties(floorCellValue, advert.Floor.ToString());

                var roomsCellValue = sheet.Cells[row, 8];
                SetCellProperties(roomsCellValue, advert.RoomsCount.ToString());

                var squareCellValue = sheet.Cells[row, 9];
                SetCellProperties(squareCellValue, advert.Area.ToString());

                row++;

            }
            sheet.Protection.IsProtected = true;

            return await package.GetAsByteArrayAsync(cancellationToken);
        }
    }

    public async Task<byte[]> CreateReportAsync(GenerateReportDto model, CancellationToken cancellationToken = default)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        var reportModels = GetModels(model);

        using (var package = new ExcelPackage())
        {
            var sheet = package.Workbook.Worksheets.Add("Отчет по закрытым сделкам");

            var mainTitle = sheet.Cells["A1:R2"];
            SetHeaderProperties(mainTitle, $"Отчет по закрытым сделкам в период с {model.DateFrom.ToString("dd.MM.yyyy")} по {model.DateTo.ToString("dd.MM.yyyy")}", 20);

            var adminName = sheet.Cells["A3:C4"];
            SetHeaderProperties(adminName, "Сотрудник", 12);

            var dateTitle = sheet.Cells["D3:F4"];
            SetHeaderProperties(dateTitle, "Дата заключения сделки", 12);

            var sumTitle = sheet.Cells["G3:I4"];
            SetHeaderProperties(sumTitle, "Прибыль агенства", 12);

            var adressTitle = sheet.Cells["J3:L4"];
            SetHeaderProperties(adressTitle, "Адрес квартиры", 12);

            var nameTitle = sheet.Cells["M3:O4"];
            SetHeaderProperties(nameTitle, "ФИО клиента", 12);

            var mailTitle = sheet.Cells["P3:R4"];
            SetHeaderProperties(mailTitle, "Адрес клиента", 12);

            int row = 5;
            var groupedList = reportModels.GroupBy(x => x.AdminName);

            foreach (var oneAdminOrders in groupedList)
            {
                int lastRow = row + oneAdminOrders.Count() - 1;

                var adminCell = sheet.Cells[row, 1, lastRow, 3];
                SetCellProperties(adminCell, oneAdminOrders.Key);
                adminCell.Style.Border.Top.Style = ExcelBorderStyle.Thick;
                adminCell.Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

                foreach (var order in oneAdminOrders)
                {
                    var dateCell = sheet.Cells[row, 4, row, 6];
                    SetCellProperties(dateCell, order.Date.ToString("dd.MM.yyyy"));

                    var sumCell = sheet.Cells[row, 7, row, 9];
                    SetCellProperties(sumCell, order.Sum.ToString());

                    var addressCell = sheet.Cells[row, 10, row, 12];
                    SetCellProperties(addressCell, order.Address);

                    var nameCell = sheet.Cells[row, 13, row, 15];
                    SetCellProperties(nameCell, order.ClientFio);

                    var mailCell = sheet.Cells[row, 16, row, 18];
                    SetCellProperties(mailCell, order.ClientMail);

                    if (row == lastRow)
                    {
                        dateCell.Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
                        sumCell.Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
                        addressCell.Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
                        nameCell.Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
                        mailCell.Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
                    }

                    row++;
                }
            }
            sheet.Protection.IsProtected = true;

            return await package.GetAsByteArrayAsync(cancellationToken);
        }
    }

    private IReadOnlyCollection<CommitedOrder> GetModels(GenerateReportDto model)
    {
        var result = new List<CommitedOrder>();
        var orders = _orderRepository.GetQuery().Where(_ => _.CommitedDate.HasValue && _.CommitedDate >= model.DateFrom.ToUniversalTime() && _.CommitedDate <= model.DateTo.ToUniversalTime()).Include(_ => _.Admin).Include(_ => _.Advert).ToList();
        foreach (var order in orders)
        {
            result.Add(new()
            {
                Address = order.Advert.Address,
                AdminName = order.Admin.Name,
                Date = order.CommitedDate!.Value,
                Sum = order.AgencySum,
                ClientFio = order.Advert.ClientFIO,
                ClientMail = order.Advert.ClientEmail
            });
        }

        result = result.OrderByDescending(_ => _.AdminName).ToList();
        return result;
    }
    private void SetCellProperties(ExcelRange cell, string value)
    {
        cell.Value = value;
        cell.Merge = true;
        cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
        cell.Style.Border.Left.Style = ExcelBorderStyle.Thick;
        cell.Style.Border.Right.Style = ExcelBorderStyle.Thick;
        cell.Style.Fill.BackgroundColor.SetColor(Color.White);
    }

    private void SetHeaderProperties(ExcelRange cell, string value, int fontSize)
    {
        cell.Value = value;
        cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
        cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        cell.Style.Font.Bold = true;
        cell.Style.Font.Size = fontSize;
        cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
        cell.Merge = true;
        cell.Style.Border.Top.Style = ExcelBorderStyle.Thick;
        cell.Style.Border.Left.Style = ExcelBorderStyle.Thick;
        cell.Style.Border.Right.Style = ExcelBorderStyle.Thick;
        cell.Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
        cell.Style.Fill.BackgroundColor.SetColor(Color.White);
    }
}
