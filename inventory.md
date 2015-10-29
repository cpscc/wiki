The Inventory Stuff
===================

###Files: 
inventory_items.mustache, located in the partials folder.
This is the basic layout of the template to be inserted on to the mustache page in place of or in lieu of the presets etc.
``` html
{{#inventory_items}}
    <div>
        <input type="hidden" name="inventory_items[id]" value="{{id}}">
        <input type="hidden" name="inventory_items[merchant]" value="{{site}}">
        <input type="hidden" name="inventory_items[disabled]" value="{{disabled}}"
        <label>{{name}}'s Available: {{items_remaining}}<input type="text" id="{{id}}" name="inventory_items[quantity]" {{#disabled}}class="disabled" placeholder="SOLD OUT" disabled{{/disabled}} onkeyup="this.value=this.value.replace(/[^0-9]/g,'')"></label>
    </div>
    <script>
        $('#'+{{id}}).keyup(function() {
            if ($(this).val() > {{items_remaining}})
                $(this).val({{items_remaining}});

            updateTotal($(this).val() * {{price}});
        });
    </script>
{{/inventory_items}}

```

This file will automatically insert any inventory items related to specific site and populate them on the page.
The process takes place in the Views.php->main_view and grabs the items and inserts them into an inventory_items array which
can be displayed using mustache.

### Required Variables 
Name | Usage
---- | -----
name | name of the item to be displayed on the page.
price | price for this item.
contact_email | the email to be contacted for the item when it reaches threshold, i.e. items_remaining = 0, or target_date is reached.
items_total | number of items the item will start with, can be adjusted to allow more items.
items_sold | number of items sold to the date.
items_remaining | the amount of items_total - items_sold.  May be adjusted to allow more items to be purchased.
target_date | target date for the item to end or be disabled.
id | the id of the item given when creating item in dashboard.  Unique.
merchant | the merchant name for the item to appropiately be associated to.
quantity | the number of items being purchased/what will be subtracted from items_remaining variable in database.
disabled | this is boolean value to disable the input and display a custom message as a placeholder as disabled, sold out, etc..
image | an image to be associated with the item.
custom details | any amount of custom details may be associated with an item for display on the page using mustache.  Added as an associative array in Views.php->main_view

###CSS
The CSS will vary and is fully customizable as with World Wide Village's page, as of right now this displays a simple inputs to be passed when the item is populated or selected and replace the input with a placeholder of `SOLD OUT` if disabled.  
This is a template, meaning it is up to you to design it as needed per customer and assign appropriate values to the page.
Example: https://give.cornerstone.cc/World+Wide+Village

###Javascript
A simple bit of jquery is inserted per item to check and see if the items being entered in the input is more than the items_remaining which is passed via the backend.  If the items attempting to be bought is > items_remaining it will reset the value to the items_remaining value. (Tested in Chrome, FireFox, Edge, IE10)

# Creating Inventory Items
Inventory Items are created via the dashboard under the Inventory tab. 

