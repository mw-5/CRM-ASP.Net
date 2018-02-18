function buildTable(data, tblId, idCol, idField, vCols)
{
    var isHeaderCreated = false;
    var htmlHeaders = "<tr>";
    var htmlRows = "";
    var rowCount = 0;

    for (var row in data)
    {
        rowCount++;
        htmlRows += "replaceMe";

        for (var col in data[row])
        {
            var cls = "col_" + col.toString().replace(/ /g, '');
            if (cls == idCol)
            {
                var rowTag = "<tr onclick='clickHandler(\"" + idField + "\", " + data[row][col] + ", \"" + tblId + "\", \"" + idCol + "\")'>";
                htmlRows = htmlRows.replace("replaceMe", rowTag);
            }

            var hiddenAttr = "";
            if (vCols.indexOf(col) == -1)
            {
                hiddenAttr = "hidden ";
            }

            if (!isHeaderCreated)
            {
                htmlHeaders += "<th " + hiddenAttr + "class='" + cls + "'><div><div>" + col + "</div><div>" + col + "</div></div></th>";
            }
            htmlRows += "<td " + hiddenAttr + "class='" + cls + "'>" + data[row][col] + "</td>";
        }
        htmlRows += "<th class='scrollbarhead'/></tr>";

        isHeaderCreated = true;
    }

    htmlHeaders += "</tr>";

    var tblHeight = rowCount * 20 + 20; // additional 20px for headers

    document.getElementById(tblId).getElementsByTagName("thead")[0].innerHTML = htmlHeaders;
    document.getElementById(tblId).getElementsByTagName("tbody")[0].innerHTML = htmlRows;
    document.getElementById(tblId).parentElement.parentElement.parentElement.setAttribute("style", "height: " + tblHeight + "px;");
}

function clickHandler(idField, idValue, tblId, idCol)
{
    document.getElementById(idField).value = idValue;
    var list = document.getElementById(tblId).getElementsByTagName("tr");
    for (var i = 0; i < list.length; i++)
    {
        list[i].classList.toggle("markedRow", false);
    }
    list = document.getElementById(tblId).getElementsByClassName(idCol);
    for (var i = 0; i < list.length; i++)
    {
        if (list[i].innerHTML == idValue)
        {
            list[i].parentElement.classList.toggle("markedRow", true);
        }
    }
}