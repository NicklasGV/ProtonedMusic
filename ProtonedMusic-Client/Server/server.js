const express = require("express");
const cors = require("cors");
const bodyparser = require("body-parser");
const stripe = require('stripe')('sk_test_51MawfMFFxCTt81aXVC5LLXg1nzTYwEQLM20LidrDRVjR3FDF3SKhazAzDgaR9871rABLvbotyuLA14hjqYmboS2x00ujPqdm9F');

const app = express();
app.use(express.static("public"));
app.use(bodyparser.urlencoded({ extended: false }));
app.use(bodyparser.json());
app.use(cors({ origin: true, credentials: true }));

//const stripe = require("stripe")("sk_test_51MawfMFFxCTt81aXVC5LLXg1nzTYwEQLM20LidrDRVjR3FDF3SKhazAzDgaR9871rABLvbotyuLA14hjqYmboS2x00ujPqdm9F");

app.post("/checkout", async (req, res, next) => {
    try {
        const session = await stripe.checkout.sessions.create({
        payment_method_types: ['card'],
        line_items: req.body.items, // Pass the received items
        mode: 'payment',
        success_url: 'http://localhost:4200/success.html', // Replace with your frontend success URL
        cancel_url: 'http://localhost:4200/cancel.html', // Replace with your frontend cancel URL
        shipping_address_collection: {
        allowed_countries: ['DK'],
        },
            shipping_options: [
            {
                shipping_rate_data: {
                type: 'fixed_amount',
                fixed_amount: {
                    amount: 5000,
                    currency: 'dkk',
                },
                display_name: 'Gratis forsendelse ',
                // Delivers between 5-7 business days
                delivery_estimate: {
                    minimum: {
                    unit: 'business_day',
                    value: 1,
                    },
                    maximum: {
                    unit: 'business_day',
                    value: 5,
                    },
                }
                }
            },
            {
                shipping_rate_data: {
                type: 'fixed_amount',
                fixed_amount: {
                    amount: 7500,
                    currency: 'dkk',
                },
                display_name: 'Levering næste dag',
                // Delivers in exactly 1 business day
                delivery_estimate: {
                    minimum: {
                    unit: 'business_day',
                    value: 1,
                    },
                    maximum: {
                    unit: 'business_day',
                    value: 1,
                    },
                }
                }
            },
            ],
           line_items:  req.body.items.map((item) => ({
            price_data: {
              currency: 'dkk',
              product_data: {
                name: item.name,
                images: [item.product]
              },
              unit_amount: item.price * 100,
            },
            quantity: item.quantity,
          })),
           mode: "payment",
        });


        res.status(200).json(session);
    } catch (error) {
      res.status(500).json({ error: error.message });
    }
});

// app.listen(4242, () => {
//     console.log('Server is running on port 4242');
//   });
