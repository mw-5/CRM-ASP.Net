function goToStart()
{
    location.href = "/Start/Start";
}

function showCustomerInCockpit(msg)
{
    navTo("/Cockpit/Search?cid=", msg);
}

function newCustomer()
{
    location.href = "/FrmCustomer/New";
}

function editCustomer(msg)
{
    navTo("/FrmCustomer/Edit?cid=", msg);
}

function navTo(url, msg)
{
    var cid = document.getElementById("selectedCid").value;

    if (cid != undefined && !isNaN(cid) && cid != 0)
    {
        location.href = url + cid;
    }
    else
    {
        alert(msg);
    }
}