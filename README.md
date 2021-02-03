# BlackdotTechnicalTest

I chose to use .NET Core MVC since that is what I usually work with.
I chose Yahoo and Bing as the two search engines which I would be getting the results from.

I created a two page application, the first page was a search bar, replicating that of the major search engines.
The second page was a list results page which diplayed the results of the search in a card-like format.
I used a WebClient to go and fetch the data and then HtmlAgilityPack to manipulate and access the HTML contents.

I added the ability to be able to select how many results would be returned using a dropdown on the first page.

Each result card on the results page showed the title of the result, the link to the result and the description.
The cards also contained a button to visit the link itself to view more information and a checkbox to select that the user wanted to download that information later.

At the bottom of the page, I added a button to download a CSV file containing links to all of the results which were selected.

I originally had the code to fetch the results from Bing and Yahoo in 2 separate code blocks but I realised that the DOM structure of both search engines was similar so I managed to put both calls in the same code block with some conditionals for the small variations that both had. 

The application can be run by downloading the project and running it directly from VisualStudio.
