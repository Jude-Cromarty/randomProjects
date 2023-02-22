package com.judecromarty.cheapshops;

import java.io.BufferedReader;
import java.io.FileReader;
import java.io.IOException;
import java.util.HashMap;
import java.util.Map;
import java.util.Scanner;

public class CheapShops {

    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);

        // Map to store prices for each item at each store
        Map<String, Map<String, Double>> prices = new HashMap<>();

        // Read in prices from CSV file
        try {
            try (BufferedReader reader = new BufferedReader(new FileReader("prices.csv"))) {
                String line = reader.readLine();
                while (line != null) {
                    String[] parts = line.split(",");
                    String item = parts[0];
                    String store = parts[1];
                    Double price = null;
                    try {
                        price = Double.valueOf(parts[2]);
                    } catch (NumberFormatException e) {
                        System.out.println("Error parsing price for " + item + " at " + store + ": " + e.getMessage());
                    }
                    if (price != null) {
                        if (!prices.containsKey(item)) {
                            prices.put(item, new HashMap<>());
                        }
                        prices.get(item).put(store, price);
                    }
                    line = reader.readLine();
                }
            }
        } catch (IOException e) {
            System.out.println("Error reading prices file: " + e.getMessage());
            return;
        }

        System.out.print("Enter items separated by a space: ");
        String input = scanner.nextLine().toLowerCase();
        String[] items = input.split("\\s+");
        for (int i = 0; i < items.length; i++) {
            items[i] = items[i].substring(0, 1).toUpperCase() + items[i].substring(1);
        }

        for (String item : items) {
            Map<String, Double> itemPrices = prices.get(item);
            if (itemPrices != null) {
                double cheapestPrice = Double.MAX_VALUE;
                String cheapestPlace = "";
                for (Map.Entry<String, Double> entry : itemPrices.entrySet()) {
                    String place = entry.getKey();
                    double price = entry.getValue();
                    if (price < cheapestPrice) {
                        cheapestPrice = price;
                        cheapestPlace = place;
                    }
                }
                System.out.println(item + " is cheapest at: " + cheapestPlace + " (£" + cheapestPrice + ")");
            } else {
                System.out.println("Sorry, we don't know the price of " + item);
            }
        }
    }
}
