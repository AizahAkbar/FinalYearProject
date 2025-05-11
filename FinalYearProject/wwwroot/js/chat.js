$(document).ready(function () {
    let isOpen = false;

    // Initialize chat state
    $('#chatContainer').hide();

    $('#chatToggle').click(function () {
        isOpen = !isOpen;
        $('#chatContainer').slideToggle();
        $('.minimize-button').text(isOpen ? '−' : '+');
        
        // Update ARIA attributes
        $('.minimize-button').attr('aria-expanded', isOpen);
        $('#chatContainer').attr('aria-hidden', !isOpen);
        
        if (isOpen) {
            $('#chatAnnouncer').text('Chat opened');
        } else {
            
            $('#chatAnnouncer').text('Chat closed');
        }
    });

    loadChatHistory();

    $('#widgetMessageInput').keypress(function (e) {
        if (e.which == 13) {
            sendMessage();
        }
    });

    $('#widgetSendButton').click(function () {
        sendMessage();
    });

    function loadChatHistory() {
        fetch('/Chat/GetChatHistory')
            .then(response => {
                if (!response.ok) {
                    throw new Error('Failed to load chat history');
                }
                return response.json();
            })
            .then(messages => {
                messages.forEach(function (message) {
                    if (message.role === 'user' || message.role === 'assistant') {
                        appendMessage(message);
                    }
                });
                scrollToBottom();
                
                //// Update message count for screen readers
                //if (messages.length > 0) {
                //    $('#widgetChatMessages').attr('aria-label', 'Chat messages, ' + messages.length + ' messages');
                //}
            })
            .catch(error => {
                console.error('Error loading chat history:', error);
            });
    }

    function sendMessage() {
        var messageInput = $('#widgetMessageInput');
        var message = messageInput.val().trim();

        if (message) {
            appendMessage({
                role: 'user',
                content: message
            });

            fetch('/Chat/SendMessage', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(message)
            })
                .then(response => {
                    if (!response.ok) {
                        throw new Error('Network response was not ok');
                    }
                    return response.json();
                })
                .then(response => {
                    if (response.role === 'assistant') {
                        appendMessage(response);
                    }
                    scrollToBottom();
                })
                .catch(error => {
                    console.error('Error sending message:', error);
                    alert('Error sending message');
                });

            messageInput.val('');
        }
    }

    function appendMessage(message) {
        // Create message element with proper ARIA attributes
        var messageHtml = `
            <div class="message ${message.role}" role="article" aria-label="${message.role} message">
                <div class="message-content">${message.content}</div>
            </div>
        `;
        $('#widgetChatMessages').append(messageHtml);
        scrollToBottom();
        
        //$('#chatAnnouncer').text(`New message from ${message.role}: ${message.content.replace(/<[^>]*>/g, '')}`);
        
        //const messageCount = $('#widgetChatMessages .message').length;
        //$('#widgetChatMessages').attr('aria-label', 'Chat messages, ' + messageCount + ' messages');
    }

    function scrollToBottom() {
        var chatMessages = $('#widgetChatMessages');
        chatMessages.scrollTop(chatMessages[0].scrollHeight);
    }
});
