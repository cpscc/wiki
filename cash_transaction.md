    <fieldset>
        <div class="switch group">
            <label class="checked"><input type=radio name=payment value=card checked> <i></i><i></i>Credit Card</label>
            <label><input type=radio name=payment value=check> <i></i><i></i>E-Check / EFT</label>
            <label><input type=radio name=payment value=cash><i></i><i></i>Cash</label>
        </div>

        <div class="card"> {{> checkout_credit}} </div>
        <div class="check group" disabled style="display: none;"> {{> checkout_ach}} </div>
        <div class="cash" disabled style="display: none;"> 
            This is simply for record keeping.<br>
            No transaction will occur through this process.
        </div>
    </fieldset>
