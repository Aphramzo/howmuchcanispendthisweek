# CoffeeScript
$(document).on 'click', '.transaction-list li', (e)->
    transaction = $ this
    confirmDelete transaction
        
confirmDelete = (transaction) ->
    transaction.addClass 'ui-btn-down-d'
    $confirm = $ '#confirm'
    $confirm.find('.location').remove()
    transaction.find('.location').clone().insertAfter '#question'
    $confirm.popup 'open'
    
    $confirm.find('#yes').on 'click', ->
        transaction.removeClass 'ui-btn-down-d'
        transaction.addClass 'right'
        transaction.on 'webkitTransitionEnd transitionend otransitionend', ->
            transaction.remove();
            #$( "#list" ).listview( "refresh" ).find( ".ui-li.border" ).removeClass( "border" );

        transaction.prev( 'li').addClass 'border'
        return
        
    
    $confirm.find('#cancel').on 'click', ->
        transaction.removeClass 'ui-btn-down-d'
        $confirm.find('#yes').off()
    
    return

        
return 

