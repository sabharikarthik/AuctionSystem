# AuctionSystem

Have created the following tables and generated models, migrations from them:

1. users
2. events
3. sold_items
4. bids

For mocking the usecase, I have created an user, event, sold_item directly in the database.

To receive bids I have created an endpoint 
    URL: POST 'api/v1/events/{event_id}/sold_items/{sold_item_id}/bids'
    Request: 
    {
        "bid": {
            "start_bid": 100,
            "max_bid": 200,
            "increment_amount": 10
        }
    }

    Response:

    200:
    {
        "success": "true"
    }

    400:
    {
        "status_code": 400,
        "message": "Error Message",
    }

    404:
    {
        "status_code": 404,
        "message": "Not Found Message"
    }

    500:
    {
        "status_code": 500,
        "message": "Error Message"
    }

Once the event end time is up, we can hit another endpoint to provide us the winning bid (In real time scenario, if it is going to take more time, we can use this endpoint to queue a job in the background and notify us with the winning bid once done, as we might need to handle n number of events and bids at the same time through the interface):

    URL: GET 'api/v1/events/{event_id}/sold_items/{sold_item_id}/generate_winning_bid'

    Response:

    200:
    {
        "winning_bid": 12000
    }

    400:
    {
        "status_code": 400,
        "message": "Error Message",
    }

    404:
    {
        "status_code": 404,
        "message": "Not Found Message"
    }

    500:
    {
        "status_code": 500,
        "message": "Error Message"
    }


Note: 
1. I haven't ignored appsettings files in the git repository for sharing purposes.
2. Followed git conventions to branch and commit