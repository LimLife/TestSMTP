//send message

const mes = {
    "subject": "Hello World",
    "body": "Random body content bal bla bla bla ",
    "recipients": ["youremail@domain", "youremail@domain", "youremail@domain"]
};


document.getElementById("btn-send").addEventListener("click", async () =>
{
    await fetch(`/api/mails`,
        {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(mes)
        });
});

document.getElementById("btn-emails").addEventListener("click", async () =>
{
    const response = await fetch("/api/mails", {
        method: "GET",
        headers: {
            "Content-Type": "application/json"
        }
    })

    const request = await response.json();

    console.log(request);
});
