Feature: Search

    As a customer
    I want be able to search for products
    So that I can order the ones I like

    @chrome @msedge
    Scenario Outline: search for a product
    Given products page is opened
    When I search for <searchterm>
    Then I should see an item with description <description> and a price <price>
    And it should be possible to add this item to my cart

    Examples:
    | searchterm | description                     | price      |
    | "t-shirt"  | "Pure Cotton V-Neck T-Shirt"    | "Rs. 1299" |
    | "cotton"   | "Pure Cotton Neon Green Tshirt" | "Rs. 850"  |