# CoffeeScript
$(document).on 'swiperight', '.transaction-list li', (e)->
    transaction = $ this
    confirmDelete transaction
        
confirmDelete = (transaction) ->
    transaction.addClass 'ui-btn-down-d'
    $confirm = $ '#confirm'
    $confirm.find('.location').remove()
    transaction.find('.location').clone().insertAfter '#question'
    $confirm.popup 'open'
    
    $confirm.find('#yes').on 'click', ->
        $.ajax '/transaction/delete', {
            type: 'POST',
            data: {id: transaction.find('input').val()}
        }
        transaction.removeClass 'ui-btn-down-d'
        transaction.addClass 'right'
        transaction.on 'webkitTransitionEnd transitionend otransitionend', ->
            transaction.remove();

        transaction.prev( 'li').addClass 'border'
        return
        
    
    $confirm.find('#cancel').on 'click', ->
        transaction.removeClass 'ui-btn-down-d'
        $confirm.find('#yes').off()
    
    return

        
return 

