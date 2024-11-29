Feature: Search product

    As a customer
    I want be able to search for products
    So that I can find specific items quickly

    @ui @api
    Scenario Outline: Search for a product that does exist
    Given Jeff is authenticated and authorised for searching products
    When Jeff searches for <searchterm>
    Then An item with description <description> and a price <price> should be returned

    Examples:
    | searchterm | description                     | price      |
    | "t-shirt"  | "Pure Cotton V-Neck T-Shirt"    | "Rs. 1299" |
#    | "cotton"   | "Pure Cotton Neon Green Tshirt" | "Rs. 850"  |

    @ui @api
    Scenario Outline: Search for a product that does not exist
    Given Jeff is authenticated and authorised for searching products
    When Jeff searches for "does not exist"
    Then No items should be returned

    @ui @api
    Scenario Outline: Search with an empty query
    Given Jeff is authenticated and authorised for searching products
    When Jeff searches for ""
    Then There should be items returned

    @manual
    # Example of scenario to be executed manually but included in this feature file so overall coverage is complete and clear.
    # Scenario can be excluded for execution by filtering on @manual tag in testrun, but this will result in scenario not being shown in testresults as well
    # If scenario is not being excluded for execution still nothing is executed, but scenario will be added in testresults which can be nice to have
    Scenario Outline: Scenario to be executed manually
    Given User is authenticated and authorised for searching products
    When User searches for "this is something to check manually"
    Then No items should be returned
