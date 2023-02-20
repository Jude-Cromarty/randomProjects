package com.JudeCromarty.cheesefinder;

import android.content.Context;
import android.content.res.AssetManager;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

import androidx.appcompat.app.AppCompatActivity;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.util.HashMap;
import java.util.Map;

public class MainActivity extends AppCompatActivity {
    private static final String TAG = "MainActivity";
    private EditText inputField;
    private TextView outputField;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        inputField = findViewById(R.id.input_field);
        outputField = findViewById(R.id.output_field);

        Button searchButton = findViewById(R.id.search_button);
        searchButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                String input = inputField.getText().toString().toLowerCase();
                String[] items = input.split("\\s+");
                for (int i = 0; i < items.length; i++) {
                    items[i] = items[i].substring(0, 1).toUpperCase() + items[i].substring(1);
                }

                // Map to store prices for each item at each store
                Map<String, Map<String, Double>> prices = new HashMap<>();

                // Read in prices from CSV file
                try {
                    InputStream inputStream = getResources().openRawResource(R.raw.prices);
                    BufferedReader reader = new BufferedReader(new InputStreamReader(inputStream));


                    String line = reader.readLine();
                    while (line != null) {
                        String[] parts = line.split(",");
                        String item = parts[0];
                        String store = parts[1];
                        Double price = null;
                        try {
                            price = Double.valueOf(parts[2]);
                        } catch (NumberFormatException e) {
                            Log.e(TAG, "Error parsing price for " + item + " at " + store + ": " + e.getMessage());
                        }
                        if (price != null) {
                            if (!prices.containsKey(item)) {
                                prices.put(item, new HashMap<>());
                            }
                            prices.get(item).put(store, price);
                        }
                        line = reader.readLine();
                    }
                    reader.close();
                } catch (IOException e) {
                    Log.e(TAG, "Error reading prices file: " + e.getMessage());
                    outputField.setText("Error reading prices file: " + e.getMessage());
                    return;
                }

                StringBuilder output = new StringBuilder();
                for (String item : items) {
                    Map<String, Double> itemPrices = prices.get(item);
                    if (itemPrices != null) {
                        double cheapestPrice = Double.MAX_VALUE;
                        String cheapestPlace = "";
                        double secondCheapestPrice = Double.MAX_VALUE;
                        String secondCheapestPlace = "";
                        for (Map.Entry<String, Double> entry : itemPrices.entrySet()) {
                            String place = entry.getKey();
                            double price = entry.getValue();
                            if (price < cheapestPrice) {
                                secondCheapestPrice = cheapestPrice;
                                secondCheapestPlace = cheapestPlace;
                                cheapestPrice = price;
                                cheapestPlace = place;
                            } else if (price < secondCheapestPrice) {
                                secondCheapestPrice = price;
                                secondCheapestPlace = place;
                            }
                        }
                        output.append(item).append(" is cheapest at: ").append(cheapestPlace).append(" (£").append(cheapestPrice).append(")");
                        if (secondCheapestPrice != Double.MAX_VALUE) {
                            output.append(", and the next cheapest is: ").append(secondCheapestPlace).append(" (£").append(secondCheapestPrice).append(")");
                        }
                        output.append("\n");
                    } else {
                        output.append("Sorry, we don't know the price of ").append(item).append("\n");
                    }
                }
                outputField.setText(output.toString());

            }
        });
    }
}
